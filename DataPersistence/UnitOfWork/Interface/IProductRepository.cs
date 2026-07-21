using UnitOfWork.Model;

namespace UnitOfWork.Interface
{
    public interface IProductRepository
    {
      Task <int> CountProductsAsync(CancellationToken cancellationToken);
      Task  AddProductAsync(Product product, CancellationToken cancellationToken);
      Task  AddReviewAsync(ProductReview review, CancellationToken cancellationToken);
      Task <int> CountProductsReviewAsync(IEnumerable<Product> products, CancellationToken cancellationToken);
      Task <bool> DeleteProductAsync(Guid productId, CancellationToken cancellationToken);
      Task <bool> DeleteReviewAsync(Guid reviewId, CancellationToken cancellationToken);
      Task <bool> ExistsByNameAsync(string? name, CancellationToken cancellationToken);
      Task <List<Product>> GetAllProductsAsync(CancellationToken cancellationToken);
      Task <List<ProductReview>> GetProductReviewsAsync(CancellationToken cancellationToken);
      Task <Product?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken);
      Task <List<Product>> GetProductPageAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
      Task <ProductReview?> GetProductReviewByIdAsync(Guid Id, CancellationToken cancellationToken);
      Task <ProductReview?> GetReviewAsync(Guid productId, Guid reviewId, CancellationToken cancellationToken);
      Task <List<ProductReview>> GetReviewsByProductIdAsync(Guid productId, CancellationToken cancellationToken);
      Task <bool> IsExistsByIdAsync(Guid id, CancellationToken cancellationToken);
      Task <bool> UpdateProductAsync(Product updatedProduct, CancellationToken cancellationToken);
      Task <bool> UpdateReviewAsync(ProductReview updatedReview, CancellationToken cancellationToken);
    }
}