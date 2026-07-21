using MethodVerb.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MethodVerb.Data
{


    public class ProductRepository
    {
        public  List<Product> Products = new List<Product>();
        public  List<ProductReview> ProductReviews = new List<ProductReview>();

        public ProductRepository()
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

            var product31 = new Product
            {
                Id = Guid.Parse("2779ee47-10b0-4bd7-8342-404006aa1392"),
                Name = $"Product {31}",
                Price = 500.0m
            };
            Products.Add(product31);



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
            var review11 = new ProductReview
            {
                Id = Guid.NewGuid(),
                ProductId = Products[30].Id,
                Reviewer = "gggg",
                Stars = 2
            };
            var review12 = new ProductReview
            {
                Id = Guid.NewGuid(),
                ProductId = Products[30].Id,
                Reviewer = "ccccc",
                Stars = 3
            };
            ProductReviews.Add(review11);
            ProductReviews.Add(review12);


        }

        public List<Product> GetAllProducts() => Products;
        public List<ProductReview> GetReviewsByProductId(Guid productId) =>
            ProductReviews.Where(r => r.ProductId == productId).ToList();


        public List<Product> GetProductPage (int page = 1 , int pageSize = 10)
        {
            return Products.Skip((page - 1) * pageSize)
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

        public ProductReview? GetProductReviewById(Guid Id)
        {
            var ProductReview = ProductReviews.FirstOrDefault(p => p.Id == Id);

            if (ProductReview is null)
                return null;

            return ProductReview;
        }
        public ProductReview? GetReview(Guid productId, Guid reviewId)
        {
            return ProductReviews.FirstOrDefault(r => r.ProductId == productId && r.Id == reviewId);
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

        public bool DeleteReview(Guid reviewId)
        {
            var review = ProductReviews.FirstOrDefault(r => r.Id == reviewId);

            if (review is null)
                return false;

            ProductReviews.Remove(review);

            return true;
        }

        public bool IsExistsById(Guid id) => Products.Any(r => r.Id == id);
        public bool ExistsByName(string? name) => Products.Any(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        public int CountProducts => Products.Count();
        public int CountProductsReview(IEnumerable<Product> products)
        {
                return products.Select(x => GetReviewsByProductId(x.Id)).Count();
        }

    }
}
