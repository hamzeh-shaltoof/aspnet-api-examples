using InMemoryCache.Data;
using InMemoryCache.Models;
using InMemoryCache.Requests;
using InMemoryCache.Responses;
using InMemoryCache.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCache.Services
{
    public class ProductService(AppDbContext context , IMemoryCache cache) : IProductService
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

            cache.Remove("products");

            return ProductResponse.FromModel(product);

        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

             context.Products.Remove(product!);


            await context.SaveChangesAsync();

            cache.Remove("products");

        }

        public async Task<ProductResponse?> GetProductByIdAsync(int productId)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            return product is null ? null : ProductResponse.FromModel(product);
        }

        public async Task<List<ProductResponse>> GetProductsAsync()
        {
            return await cache.GetOrCreate("products", async (entry) =>
            {
                // there is no cache

                entry.Size = 1;

                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30); // TTL


                Console.WriteLine("DB visited");

                return (await context.Products.ToListAsync())
                   .Select(p => ProductResponse.FromModel(p)).ToList() ?? [];

            })!;

        }
        public async Task<List<ProductResponse>> GetProductsAsync_Old()
        {
            var cacheKey = "products";

            if (cache.TryGetValue(cacheKey, out List<ProductResponse>? products))
            {
                // Their Found Proudct In Cache
                Console.WriteLine("Cache Visited");
                return products!;

            }

            products = await context.Products.Select(x => ProductResponse.FromModel(x)).ToListAsync() ?? [];
            Console.WriteLine("DataBase Visited");
            cache.Set(cacheKey, products, new MemoryCacheEntryOptions()
            {
                Size = 1,
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) // TTL
            });
            return products;

        }

        public async Task UpdateProductAsync(int productId, UpdateProductRequest request)
        {
            var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == productId)
                                    ?? throw new KeyNotFoundException("product not found");

            existingProduct.Name = request.Name;

            existingProduct.Price = request.Price;

            await context.SaveChangesAsync();
            cache.Remove("products");     // Invalidate
        }
    }
}
