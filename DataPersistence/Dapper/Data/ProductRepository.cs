using Dapper;
using DapperMicroOptimizations.Data;
using DapperMicroOptimizations.Model;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DapperMicroOptimizations.Data
{


    public class ProductRepository(IDbConnection db)
    {
        public  List<Product> Products = new List<Product>();
        public  List<ProductReview> ProductReviews = new List<ProductReview>();

       

        public List<Product> GetAllProducts() => db.Query<Product>("SELECT * FROM products").ToList();
        public List<ProductReview> GetReviewsByProductId(Guid productId) =>
            db.Query<ProductReview>(
                "SELECT * FROM ProductReviews WHERE productId = @ProductId",
                new { ProductId = productId.ToString() }
            ).ToList();


        public List<Product> GetProductPage (Guid? lastSeenId, int pageSize = 10)
        {

            return db.Query<Product>(
                "SELECT * FROM products WHERE(@LastSeenId IS NULL OR Id > @LastSeenId) ORDER BY Id LIMIT @Limit",
                new { Limit = pageSize, LastSeenId = lastSeenId == Guid.Empty ? null : lastSeenId.ToString() }).ToList();   
        }

        public Product? GetProductById(Guid productId)
        {

            var product = db.QuerySingleOrDefault<Product>(
                                         "SELECT * FROM Products WHERE Id = @Id",
                                          new { Id = productId.ToString() });

            if (product is null)
                return null;

            return product;
        }

        public ProductReview? GetProductReviewById(Guid id)
        {
            var ProductReview = db.QuerySingleOrDefault<ProductReview>(
                "SELECT * FROM ProductReviews WHERE productId = @Id",
                new { Id = id.ToString()});

            if (ProductReview is null)
                return null;

            return ProductReview;
        }
        public ProductReview? GetReview(Guid productId, Guid reviewId)
        {
            return db.QuerySingleOrDefault<ProductReview>(
                "SELECT * FROM ProductReviews WHERE ProductId = @ProductId AND Id = ReviewId" 
                                , new { ProductId = productId .ToString() , ReviewId = reviewId.ToString() ,} );
        }
        public void AddProduct(Product product)
        {
            if (product.Id == Guid.Empty)
            {
                product.Id = Guid.NewGuid();
            }

            string query = @"
        INSERT INTO products (Id, Name, Price) 
        VALUES (@Id, @Name, @Price)";

            db.Execute(query, product);
        }

        public bool UpdateProduct(Product updatedProduct)
        {
            string query = @"
        UPDATE products 
        SET Name = @Name, 
            Price = @Price 
        WHERE Id = @Id";

            int rowsAffected = db.Execute(query, updatedProduct);

            return rowsAffected > 0;
        }

        public bool DeleteProduct(Guid productId)
        {
            string query = @"
        DELETE FROM ProductReviews WHERE ProductId = @ProductId;
        DELETE FROM products WHERE Id = @ProductId;";

            int rowsAffected = db.Execute(query, new { ProductId = productId.ToString() });

            return rowsAffected > 0;
        }
        public void AddReview(ProductReview review)
        {
            if (review.Id == Guid.Empty)
            {
                review.Id = Guid.NewGuid();
            }

            string query = @"
                              INSERT INTO ProductReviews (Id, ProductId, Comment, Rating) 
                              VALUES (@Id, @ProductId, @Comment, @Rating)";

            db.Execute(query, review);
        }
        public bool UpdateReview(ProductReview updatedReview)
        {
            var existingReview = ProductReviews.FirstOrDefault(r => r.Id == updatedReview.Id);

            if (existingReview is null)
                return false;

            existingReview.Reviewer = updatedReview.Reviewer;
            existingReview.Stars = updatedReview.Stars;

            db.Execute(@"UPDATE ProductReviews SET Reviewer = @Reviewer, Stars = @Stars WHERE Id = @Id",
                 new { Reviewer = updatedReview.Reviewer, Stars = updatedReview.Stars, Id = updatedReview.Id.ToString() });

            return true;
        }

        public bool DeleteReview(Guid reviewId)
        {
            var review = db.ExecuteScalar<bool>
                (@"DELETE FROM ProductReviews WHERE Id =@Id "
                , new {Id = reviewId.ToString() });

                return review;
        }

        public bool IsExistsById(Guid id) =>
            db.ExecuteScalar<bool>(@"SELECT 1  FROM Products WHERE Id = @Id",
                new { Id = id.ToString() });
        public bool ExistsByName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            string query = @"SELECT EXISTS(SELECT 1 FROM Products WHERE Name LIKE @Name)";

            return db.ExecuteScalar<bool>(query, new { Name = name });
        }
        public int CountProducts => db.ExecuteScalar<int>("SELECT COUNT(*) FROM Products");
        public int CountProductsReview(IEnumerable<Product> products)
        {
                return products.Select(x => GetReviewsByProductId(x.Id)).Count();
        }

    }
}
