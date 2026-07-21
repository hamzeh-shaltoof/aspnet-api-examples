using HybridCache;
using HybridCache.Data;
using HybridCache.Models;
using HybridCache.Requests;
using HybridCache.Responses;
using HybridCache.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace HybridCache.Services
{
    public class ProductService(AppDbContext context , Microsoft.Extensions.Caching.Hybrid.HybridCache cache, IConfiguration configuration) : IProductService
    {
        public  async Task<ProductResponse> AddProductAsync(CreateProductRequest request)
        {
      

            var product = new Product
            {
                Name = request.Name,
                Price = request.Price
            };

           await context.Products.AddAsync(product);
           await context.SaveChangesAsync();

           await cache.RemoveAsync("products-tag");  // Invalidate

            return ProductResponse.FromModel(product);

        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

             context.Products.Remove(product!);


            await context.SaveChangesAsync();

           await cache.RemoveAsync("products-tag");  // Invalidate

        }

        public async Task<ProductResponse?> GetProductByIdAsync(int productId)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            return product is null ? null : ProductResponse.FromModel(product);
        }

        public async Task<List<ProductResponse>> GetProductsAsync()
        {
            var cacheKey = CacheKeys.Products;

            // Four Parameter => GetOrCreateAsync(Key , (cancellationToken)=>{logic} , Configuration , Tages)
            return await cache.GetOrCreateAsync(cacheKey,async cancellationToken =>
            {
                var entities = await context.Products.ToListAsync(cancellationToken);

                Console.WriteLine("DB Visited");

                return entities.Select(x => ProductResponse.FromModel(x)).ToList() ?? [];

            },
            options: new HybridCacheEntryOptions
            {

            },
            tags: ["products-tag"]);

        }
       

        public async Task UpdateProductAsync(int productId, UpdateProductRequest request)
        {
            var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == productId)
                                    ?? throw new KeyNotFoundException("product not found");

            existingProduct.Name = request.Name;

            existingProduct.Price = request.Price;

            await context.SaveChangesAsync();
            await cache.RemoveAsync("products-tag");    // Invalidate
        }
    }
}
