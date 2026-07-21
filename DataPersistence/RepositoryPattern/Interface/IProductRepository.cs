using MethodVerb.Model;

namespace MethodVerb.Interface
{
    public interface IProductRepository
    {
        int CountProducts { get; }

        void AddProduct(Product product);
        void AddReview(ProductReview review);
        int CountProductsReview(IEnumerable<Product> products);
        bool DeleteProduct(Guid productId);
        bool DeleteReview(Guid reviewId);
        bool ExistsByName(string? name);
        List<Product> GetAllProducts();
        List<ProductReview> GetProductReviews();
        Product? GetProductById(Guid productId);
        List<Product> GetProductPage(int page = 1, int pageSize = 10);
        ProductReview? GetProductReviewById(Guid Id);
        ProductReview? GetReview(Guid productId, Guid reviewId);
        List<ProductReview> GetReviewsByProductId(Guid productId);
        bool IsExistsById(Guid id);
        bool UpdateProduct(Product updatedProduct);
        bool UpdateReview(ProductReview updatedReview);
    }
}