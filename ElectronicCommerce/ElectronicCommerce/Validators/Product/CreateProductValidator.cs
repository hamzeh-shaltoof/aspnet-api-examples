using ElectronicCommerce.DTO.Product;
using ElectronicCommerce.Entities;
using ElectronicCommerce.Helpers;
using FluentValidation;

namespace ElectronicCommerce.Validators.Product
{
    public class CreateProductValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                   .NotEmpty().WithMessage("Product Name Is Required.")
                   .MaximumLength(40).WithMessage("Product Name Cannot Exceed 40 Characters.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Image URL Is Required.")
                .Must(ValidationHelpers.IsUrl).WithMessage("Invalid image URL format.");

            RuleFor(x => x.Description)
                .MaximumLength(450).WithMessage("Description Cannot Exceed 450 Characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
  
    }
}
