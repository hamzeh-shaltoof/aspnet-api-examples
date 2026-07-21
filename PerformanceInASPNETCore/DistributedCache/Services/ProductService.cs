using DistributedCache.Data;
using DistributedCache.Models;
using DistributedCache.Requests;
using DistributedCache.Responses;
using DistributedCache.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace DistributedCache.Services
{
    public class ProductService(AppDbContext context , IDistributedCache cache, IConfiguration configuration) : IProductService
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

           await cache.RemoveAsync("products");  // Invalidate

            return ProductResponse.FromModel(product);

        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

             context.Products.Remove(product!);


            await context.SaveChangesAsync();

           await cache.RemoveAsync("products");  // Invalidate

        }

        public async Task<ProductResponse?> GetProductByIdAsync(int productId)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            return product is null ? null : ProductResponse.FromModel(product);
        }

        public async Task<List<ProductResponse>> GetProductsAsync()
        {
            var cacheKey = CacheKeys.Products;

          var cacheData =  await cache.GetStringAsync(cacheKey!);
            if(cacheData is not null)
            {
                // Their List Product Found In Redis Cache
                Console.WriteLine("Radis Visited");
                return JsonSerializer.Deserialize<List<ProductResponse>>(cacheData!);
            }
            Console.WriteLine("DataBase Cache Visited");
            var entities = await context.Products.ToListAsync();

            var products = entities.Select(ProductResponse.FromModel).ToList();

            var jsonData = JsonSerializer.Serialize<List<ProductResponse>>(products);

            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };
            await cache.SetStringAsync(cacheKey!,jsonData,options);

            return products;

        }
       

        public async Task UpdateProductAsync(int productId, UpdateProductRequest request)
        {
            var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == productId)
                                    ?? throw new KeyNotFoundException("product not found");

            existingProduct.Name = request.Name;

            existingProduct.Price = request.Price;

            await context.SaveChangesAsync();
            await cache.RemoveAsync("products");    // Invalidate
        }
    }
}
