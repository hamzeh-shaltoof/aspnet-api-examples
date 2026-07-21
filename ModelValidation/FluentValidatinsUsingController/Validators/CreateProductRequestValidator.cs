using FluentValidatinsUsingController.Enums;
using FluentValidatinsUsingController.Requests;
using FluentValidation;

namespace FluentValidatinsUsingController.Validators
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                                .WithMessage("Product name is required.")
                                .Length(3, 100)
                                .WithMessage("Product name must be between 3 and 100 characters.");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Product description is required.")
                                       .MaximumLength(1000)
                                       .WithMessage("Product description cannot exceed 1000 characters.");

            RuleFor(x => x.SKU).NotEmpty().WithMessage("SKU is required.")
                               .Matches(@"PRD-\d{5}")
                               .WithMessage("SKU format must match 'PRD-XXXXX' (e.g., PRD-12345).");

            RuleFor(x => x.Price).GreaterThan(0)
                                 .WithMessage("Price must be greater than 0.");

            RuleFor(x => x.StockQuantity).GreaterThanOrEqualTo(0)
                                         .WithMessage("Stock quantity cannot be negative.");

            RuleFor(x => x.LaunchDate).Must(MustBeNowOrFuture)
                                      .WithMessage("Launch date must be today or in the future.");

            RuleFor(x => x.Category).IsInEnum()
                                    .WithMessage("Invalid category selected.");

            RuleFor(x => x.ImageUrl).Must(IsUrl!)
                                    .WithMessage("Image URL format is invalid.");

            RuleFor(x => x.ImageUrl).Must(IsUrl!)
                                    .WithMessage("Image URL format is invalid.");

            RuleFor(x => x.Weight).InclusiveBetween(0.001m, 1000m)
                                  .WithMessage("Weight must be between 0.001 and 1000.");

            RuleFor(x => x.WarrantyPeriodMonths).Must(MustBe_1_2_3)
                                                .WithMessage("Warranty period must be either 1, 2, or 3 months.");

            When(x => x.IsReturnable, () =>
            {
                RuleFor(x => x.ReturnPolicyDescription).NotEmpty()
                                                        .WithMessage("Return policy description is required when the product is returnable.");
            });

            RuleFor(x => x.Tags).Must(tages => tages.Count <= 5)
                                .WithMessage("Product must have Smalest than 5 tags.");
        }
        public bool MustBe_1_2_3(int period)
            => period is 12 or 24 or 36;

        public bool IsUrl(string uri)
            => Uri.TryCreate(uri, UriKind.Absolute, out _);

        public bool MustBeNowOrFuture(DateTime time)
           => time.Date >= DateTime.UtcNow.Date;
    }
}
