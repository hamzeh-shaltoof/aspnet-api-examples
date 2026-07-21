using System.ComponentModel.DataAnnotations;

namespace DataAnnotationValidationUsingMinimalAPI.Validators
{
    public static class WarrantyValidators
    {
        public static ValidationResult? MustBe_0_1_2_3(int value , ValidationContext validationContext)
        {
            if (value is 0 or 12 or 24 or 36)
                return ValidationResult.Success;

            return new ValidationResult("Must Be Yearly Quit", [validationContext.MemberName!]);
        }
    }
}
