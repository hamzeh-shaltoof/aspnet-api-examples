using System.ComponentModel.DataAnnotations;

namespace DataAnnotationValidation.Validators
{
    public class RequiredIfAttribute : ValidationAttribute
    {
        private readonly string? _dependencyProperty; // IsReturnable
        private readonly object? _targetValue; // true  مثبتة دايما

        public RequiredIfAttribute(string dependencyProperty , object? targetValue)
        {
            this._dependencyProperty = dependencyProperty;
            this._targetValue = targetValue;    // Take in 
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) 
            // object? value Is { ReturnPolicyDescription = Value Request (Content) }   // هاي قيمة جاية من كلاس المنتج
        {
            var containerType = validationContext.ObjectInstance.GetType(); // containerType = CreateProductRequest 
            var property = containerType.GetProperty(this._dependencyProperty!); // property = IsReturnable

            if (property is null)
                    return new ValidationResult($"Uknown Propery {property}");

            var dependentValue = property.GetValue(validationContext.ObjectInstance, null); // IsReturnable = Value Request (true Or false)  // هاي قيمة جاية من كلاس المنتج

            if (Equals(dependentValue, _targetValue))
                if (value is null || (value is string content && string.IsNullOrEmpty(content)))
                    return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} Is Required "); // Display Propery ReturnPolicyDescription As Name 

            return ValidationResult.Success;

        }
    }
}
