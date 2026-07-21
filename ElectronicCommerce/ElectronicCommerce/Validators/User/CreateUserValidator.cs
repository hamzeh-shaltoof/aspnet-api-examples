using ElectronicCommerce.DTO.User;
using ElectronicCommerce.Helpers;
using FluentValidation;

namespace ElectronicCommerce.Validators.User
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(20).WithMessage("First name cannot exceed 20 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(20).WithMessage("Last name cannot exceed 20 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Please enter a valid email address (e.g., name@example.com).");

            RuleFor(x => x.BirthDate)
                .Must(ValidationHelpers.IsAdult).WithMessage("You must be 18 years or older to register.")
                .When(x => x.BirthDate.HasValue);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter (a-z).")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter (A-Z).")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number (0-9).")
                .Matches(@"[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character (e.g., @, #, $, %).");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Please confirm your password.")
                .Equal(x => x.Password).WithMessage("Passwords do not match.")
                .When(x => !string.IsNullOrEmpty(x.Password)); 

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^07[6789][0-9]{7}$").WithMessage("Please enter a valid phone number valid")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

            RuleFor(x => x.ProfilePictureUrl)
                .Must(ValidationHelpers.IsUrl)
                .WithMessage("Please enter a valid image URL.")
                .When(x => !string.IsNullOrEmpty(x.ProfilePictureUrl));
                
        }
    }
}
