using System.ComponentModel.DataAnnotations;

namespace DataAnnotationValidationUsingMinimalAPI.Validators
{
    public static class LaunchDateValidators
    {
        public static ValidationResult? MustBeTodayOrFuture(DateTime dateTime, ValidationContext validationContext)
        {

            if(dateTime.Date >= DateTime.UtcNow.Date)
                return ValidationResult.Success;

            return new ValidationResult("Must Be LaunchDate Today Or Future",
                       [validationContext.MemberName!]);

        }
    }
}
