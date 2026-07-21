using CancellationTokens.Interface;
using CancellationTokens.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationTokens.Data
{


    public class EFProductRepository(AppDbContext context) : IProductRepository
    {
        public async Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken) => await context.Products.ToListAsync(cancellationToken);
        public async Task<List<ProductReview>> GetProductReviewsAsync(CancellationToken cancellationToken) => await context.ProductReviews.ToListAsync(cancellationToken);
        public async Task<List<ProductReview>> GetReviewsByProductIdAsync(Guid productId, CancellationToken cancellationToken) =>
          await  context.ProductReviews.Where(r => r.ProductId == productId).ToListAsync(cancellationToken);


        public async Task<List<Product>> GetProductPageAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            return await context.Products.Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);

            if (product is null)
                return null;

            return product;
        }

        public async Task<ProductReview?> GetProductReviewByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            var ProductReview = await context.ProductReviews.FirstOrDefaultAsync(p => p.Id == Id, cancellationToken);

            if (ProductReview is null)
                return null;

            return ProductReview;
        }
        public async Task<ProductReview?> GetReviewAsync(Guid productId, Guid reviewId, CancellationToken cancellationToken)
        {
            return await context.ProductReviews.FirstOrDefaultAsync
                            (r => r.ProductId == productId && r.Id == reviewId, cancellationToken);
        }
        public async Task AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            if (product.Id == Guid.Empty)
            {
                product.Id = Guid.NewGuid();
            }
           await context.Products.AddAsync(product, cancellationToken);
           await context.SaveChangesAsync(cancellationToken);

        }

       
        public async Task<bool> UpdateProductAsync(Product updatedProduct, CancellationToken cancellationToken)
        {
            var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == updatedProduct.Id, cancellationToken);

            if (existingProduct is null)
                return false;

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;


            context.Products.Update(existingProduct);

           await context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> DeleteProductAsync(Guid productId, CancellationToken cancellationToken)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);

            if (product is null)
                return false;

            context.Products.Remove(product);
           await context.SaveChangesAsync(cancellationToken);



            return true;
        }

        public async Task AddReviewAsync(ProductReview review, CancellationToken cancellationToken)
        {
            if (review.Id == Guid.Empty)
            {
                review.Id = Guid.NewGuid();
            }

           await context.ProductReviews.AddAsync(review, cancellationToken);
           await context.SaveChangesAsync(cancellationToken);

        }

        public async Task<bool> UpdateReviewAsync(ProductReview updatedReview, CancellationToken cancellationToken)
        {
            var existingReview = await context.ProductReviews.FirstOrDefaultAsync(r => r.Id == updatedReview.Id, cancellationToken);

            if (existingReview is null)
                return false;

            existingReview.Reviewer = updatedReview.Reviewer;
            existingReview.Stars = updatedReview.Stars;

            context.ProductReviews.Update(existingReview);

            return true;
        }

        public async Task<bool> DeleteReviewAsync(Guid reviewId, CancellationToken cancellationToken)
        {
            var review = await context.ProductReviews.FirstOrDefaultAsync(r => r.Id == reviewId);

            if (review is null)
                return false;

            context.ProductReviews.Remove(review);
           await context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> IsExistsByIdAsync(Guid id,CancellationToken cancellationToken) => await context.Products.AnyAsync(r => r.Id == id, cancellationToken);
        public async Task<bool> ExistsByNameAsync(string? name, CancellationToken cancellationToken) => await context.Products.AnyAsync(x => EF.Functions.Like(name!.ToLower(), x.Name!.ToLower()), cancellationToken);
        public async Task<int> CountProductsAsync(CancellationToken cancellationToken) => await context.Products.CountAsync(cancellationToken);

        public async Task<int> CountProductsReviewAsync(IEnumerable<Product> products, CancellationToken cancellationToken)
        {
            var productIds = products.Select(p => p.Id).ToList();
            return await context.ProductReviews.CountAsync(r => productIds.Contains(r.ProductId), cancellationToken);
        }
    }
}
