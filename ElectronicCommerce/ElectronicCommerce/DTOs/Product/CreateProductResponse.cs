using ElectronicCommerce.DTO.Product;

namespace ElectronicCommerce.DTOs.Product
{
    public class CreateProductResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public string ImageUrl { get; init; } = null!;
        public bool IsAvailable { get; init; }
        public decimal DiscountRate { get; init; }
        public string? Description { get; init; } = null!;
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
        private CreateProductResponse() { }

        public static CreateProductResponse Create(Entities.Product product) {

            if (product == null) 
                throw new ArgumentNullException(nameof(product));

             return new CreateProductResponse()
             {
                 Id = product.Id,
                 Name = product.Name,
                 ImageUrl = product.ImageUrl,
                 DiscountRate = product.DiscountRate,
                 Description = product.Description,
                 IsAvailable = product.IsAvailable,
                 CategoryId = product.CategoryId,
                 CreatedAt = product.CreatedAt,
                 UpdatedAt = product.UpdatedAt,
             };
        }

    }

}
