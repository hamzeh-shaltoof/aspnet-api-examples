using URLPathVersioningController.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace URLPathVersioningController.Data
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
                    Id = i,
                    Name = $"Product {i}",
                    UnitPrice = (decimal)(new Random().Next(10, 500) + new Random().NextDouble()),
                    CurrentPrice = "JO",
                    Quantity = (new Random().Next(1, 20000))
                };
                Products.Add(product);
            }

            string[] firstNames = { "Ahmed", "Sara", "Hamzeh", "Yousef", "Rania", "Omar", "Tamer", "Sami" };
            string[] lastNames = {  "Mansour", "Al-Alami", "Haddad", "Abdel-Nabi", "Khatib", "Nasser", "Zaid" };

            for (int i = 0; i < 10; i++)
            {
                var review = new ProductReview
                {
                    Id = Guid.NewGuid(),
                    ProductId = Products[i].Id,
                    FirstName = firstNames[new Random().Next(firstNames.Length)],
                    LastName = lastNames[new Random().Next(lastNames.Length)],
                    Stars = new Random().Next(1, 6) 
                };
                ProductReviews.Add(review);
            }
        }

        public List<Product> GetAllProducts() => Products;
        public List<ProductReview> GetReviewsByProductId(int productId) =>
            ProductReviews.Where(r => r.ProductId == productId).ToList();


        public List<Product> GetProductPage (int page = 1 , int pageSize = 10)
        {
            return Products.Skip((page - page - 1) * pageSize)
                           .Take(pageSize)
                           .ToList();
        }

        public Product? GetProductById(int productId)
        {
            var product = Products.FirstOrDefault(p => p.Id == productId);

            if (product is null)
                return null;

            return product;
        }

        public ProductReview? GetProductReviewById(int productId)
        {
            var ProductReview = ProductReviews.FirstOrDefault(p => p.ProductId == productId);

            if (ProductReview is null)
                return null;

            return ProductReview;
        }

  
      



        public void AddReview(ProductReview review)
        {
            if (review.Id == Guid.Empty)
            {
                review.Id = Guid.NewGuid();
            }
            ProductReviews.Add(review);
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
