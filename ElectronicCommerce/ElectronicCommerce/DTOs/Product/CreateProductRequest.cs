using ElectronicCommerce.Entities;
using FluentValidation;

namespace ElectronicCommerce.DTO.Product
{
    public class CreateProductRequest

    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public bool IsAvailable { get; set; }
        public decimal DiscountRate { get; set; }
        public string? Description { get; set; } = null!;
    }
}
