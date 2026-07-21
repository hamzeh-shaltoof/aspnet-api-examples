using Dapper;
using RepositoryPattern.Data;
using RepositoryPattern.Model;
using RepositoryPattern.Interface;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RepositoryPattern.Data
{


    public class DapperProductRepository(IDbConnection db) : IProductRepository
    {
        public List<Product> GetAllProducts() => db.Query<Product>("SELECT Id, Name, Price FROM products").ToList();
        public List<ProductReview> GetProductReviews() => db.Query<ProductReview>("SELECT * FROM ProductReviews ").ToList();
        public List<ProductReview> GetReviewsByProductId(Guid productId) =>
            db.Query<ProductReview>(
                "SELECT * FROM ProductReviews WHERE productId = @ProductId COLLATE NOCASE",
                new { ProductId = productId.ToString() }
            ).ToList();


        public List<Product> GetProductPage(int page = 1, int pageSize = 10)
        {
         
            string sql = "SELECT Id, Name, Price FROM products ORDER BY rowid LIMIT @Limit OFFSET @Offset";

            return db.Query<Product>(sql, new
            {
                Limit = pageSize,
                Offset = (page - 1) * pageSize
            }).ToList();
        }

        public Product? GetProductById(Guid productId)
        {

            var product = db.QuerySingleOrDefault<Product>(
                                         "SELECT * FROM Products WHERE Id = @Id COLLATE NOCASE",
                                          new { Id = productId.ToString() });

            if (product is null)
                return null;

            return product;
        }

        public ProductReview? GetProductReviewById(Guid id)
        {
            var ProductReview = db.QuerySingleOrDefault<ProductReview>(
                "SELECT * FROM ProductReviews WHERE Id = @Id COLLATE NOCASE",
                new { Id = id.ToString()});

            if (ProductReview is null)
                return null;

            return ProductReview;
        }
        public ProductReview? GetReview(Guid productId, Guid reviewId)
        {
            return db.QuerySingleOrDefault<ProductReview>(
                "SELECT * FROM ProductReviews WHERE ProductId = @ProductId COLLATE NOCASE AND Id = @ReviewId COLLATE NOCASE"
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
        DELETE FROM ProductReviews WHERE ProductId = @ProductId COLLATE NOCASE;
        DELETE FROM products WHERE Id = @ProductId COLLATE NOCASE;";

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
            var existingReview = GetProductReviewById(updatedReview.Id);

            if (existingReview is null)
                return false;

            existingReview.Reviewer = updatedReview.Reviewer;
            existingReview.Stars = updatedReview.Stars;

            db.Execute(@"UPDATE ProductReviews SET Reviewer = @Reviewer, Stars = @Stars WHERE Id = @Id COLLATE NOCASE",
                 new { Reviewer = updatedReview.Reviewer, Stars = updatedReview.Stars, Id = updatedReview.Id.ToString() });

            return true;
        }

        public bool DeleteReview(Guid reviewId)
        {
            int rowsAffected = db.Execute(@"DELETE FROM ProductReviews WHERE Id = @Id COLLATE NOCASE",
                new { Id = reviewId.ToString() });
            return rowsAffected > 0;
        }

        public bool IsExistsById(Guid id) =>
            db.ExecuteScalar<bool>(@"SELECT 1  FROM Products WHERE Id = @Id COLLATE NOCASE",
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
