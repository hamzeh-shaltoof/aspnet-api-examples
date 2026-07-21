using InitialControllerSetup.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialControllerSetup.Data
{


    public class ProductRepository
    {
        public static List<Product> Products = new List<Product>();
        public static List<ProductReview> ProductReviews = new List<ProductReview>();

        static ProductRepository()
        {
           
            for (int i = 1; i <= 30; i++)
            {
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = $"Product {i}",
                    Price = (decimal)(new Random().Next(10, 500) + new Random().NextDouble())
                };
                Products.Add(product);
            }

            string[] names = { "Ahmed", "Sara", "John", "Mona", "Ali" ,"hamzeh", "tammer", "sami" };

            for (int i = 0; i < 10; i++)
            {
                var review = new ProductReview
                {
                    Id = Guid.NewGuid(),
                    ProductId = Products[i].Id,
                    Reviewer = names[new Random().Next(names.Length)],
                    Stars = new Random().Next(1, 6) 
                };
                ProductReviews.Add(review);
            }
        }

        public List<Product> GetAllProducts() => Products;
        public List<ProductReview> GetReviewsByProductId(Guid productId) =>
            ProductReviews.Where(r => r.ProductId == productId).ToList();


        public List<Product> GetProductPage (int page = 1 , int pageSize = 10)
        {
            return Products.Skip((page - page - 1) * pageSize)
                           .Take(pageSize)
                           .ToList();
        }

        public Product? GetProductById(Guid productId)
        {
            var product = Products.FirstOrDefault(p => p.Id == productId);

            if (product is null)
                return null;

            return product;
        }

        public ProductReview? GetProductReviewById(Guid productId)
        {
            var ProductReview = ProductReviews.FirstOrDefault(p => p.Id == productId);

            if (ProductReview is null)
                return null;

            return ProductReview;
        }

        public void AddProduct(Product product)
        {
            if (product.Id == Guid.Empty)
            {
                product.Id = Guid.NewGuid();
            }
            Products.Add(product);
        }

      
        public bool UpdateProduct(Product updatedProduct)
        {
            var existingProduct = Products.FirstOrDefault(p => p.Id == updatedProduct.Id);

            if (existingProduct is null)
                return false;

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;

            return true;
        }

        public bool DeleteProduct(Guid productId)
        {
            var product = Products.FirstOrDefault(p => p.Id == productId);

            if (product is null)
                return false;

            Products.Remove(product);

           
            ProductReviews.RemoveAll(r => r.ProductId == productId);

            return true;
        }

        public void AddReview(ProductReview review)
        {
            if (review.Id == Guid.Empty)
            {
                review.Id = Guid.NewGuid();
            }
            ProductReviews.Add(review);
        }

        public bool UpdateReview(ProductReview updatedReview)
        {
            var existingReview = ProductReviews.FirstOrDefault(r => r.Id == updatedReview.Id);

            if (existingReview is null)
                return false;

            existingReview.Reviewer = updatedReview.Reviewer;
            existingReview.Stars = updatedReview.Stars;

            return true;
        }

        // 4. حذف تقييم معين
        public bool DeleteReview(Guid reviewId)
        {
            var review = ProductReviews.FirstOrDefault(r => r.Id == reviewId);

            if (review is null)
                return false;

            ProductReviews.Remove(review);

            return true;
        }

        public bool ReviewExistsById(Guid id) => ProductReviews.Any(r => r.Id == id);
        public bool ExistsByName(string? name) => Products.Any(p => p.Name == name);
    }
}
