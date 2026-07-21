using ElectronicCommerce.DTO.Product;
using ElectronicCommerce.Entities;
using ElectronicCommerce.Helpers;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace ElectronicCommerce.Validators.Product
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Product ID Is Required.")
                .GreaterThan(0).WithMessage("Product ID Must be Greater Than 0.");

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
