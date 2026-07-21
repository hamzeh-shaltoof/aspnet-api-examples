using RepositoryPattern.Interface;
using RepositoryPattern.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryPattern.Data
{


    public class EFProductRepository(AppDbContext context) : IProductRepository
    {
        public List<Product> GetAllProducts() => context.Products.ToList();
        public List<ProductReview> GetProductReviews() => context.ProductReviews.ToList();
        public List<ProductReview> GetReviewsByProductId(Guid productId) =>
            context.ProductReviews.Where(r => r.ProductId == productId).ToList();


        public List<Product> GetProductPage(int page = 1, int pageSize = 10)
        {
            return context.Products.Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .ToList();
        }

        public Product? GetProductById(Guid productId)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == productId);

            if (product is null)
                return null;

            return product;
        }

        public ProductReview? GetProductReviewById(Guid Id)
        {
            var ProductReview = context.ProductReviews.FirstOrDefault(p => p.Id == Id);

            if (ProductReview is null)
                return null;

            return ProductReview;
        }
        public ProductReview? GetReview(Guid productId, Guid reviewId)
        {
            return context.ProductReviews.FirstOrDefault(r => r.ProductId == productId && r.Id == reviewId);
        }
        public void AddProduct(Product product)
        {
            if (product.Id == Guid.Empty)
            {
                product.Id = Guid.NewGuid();
            }
            context.Products.Add(product);
            context.Products.Add(product);
            context.SaveChanges();

        }


        public bool UpdateProduct(Product updatedProduct)
        {
            var existingProduct = context.Products.FirstOrDefault(p => p.Id == updatedProduct.Id);

            if (existingProduct is null)
                return false;

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;


            context.Products.Update(existingProduct);

            context.SaveChanges();

            return true;
        }

        public bool DeleteProduct(Guid productId)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == productId);

            if (product is null)
                return false;

            context.Products.Remove(product);
            context.SaveChanges();



            return true;
        }

        public void AddReview(ProductReview review)
        {
            if (review.Id == Guid.Empty)
            {
                review.Id = Guid.NewGuid();
            }

            context.ProductReviews.Add(review);
            context.SaveChanges();

        }

        public bool UpdateReview(ProductReview updatedReview)
        {
            var existingReview = context.ProductReviews.FirstOrDefault(r => r.Id == updatedReview.Id);

            if (existingReview is null)
                return false;

            existingReview.Reviewer = updatedReview.Reviewer;
            existingReview.Stars = updatedReview.Stars;

            context.ProductReviews.Update(existingReview);

            return true;
        }

        public bool DeleteReview(Guid reviewId)
        {
            var review = context.ProductReviews.FirstOrDefault(r => r.Id == reviewId);

            if (review is null)
                return false;

            context.ProductReviews.Remove(review);
            context.SaveChanges();


            return true;
        }

        public bool IsExistsById(Guid id) => context.Products.Any(r => r.Id == id);
        public bool ExistsByName(string? name) => context.Products.Any(x => EF.Functions.Like(name!.ToLower(), x.Name!.ToLower()));
        public int CountProducts => context.Products.Count();
        public int CountProductsReview(IEnumerable<Product> products)
        {
            return products.Select(x => GetReviewsByProductId(x.Id)).Count();
        }

    }
}
