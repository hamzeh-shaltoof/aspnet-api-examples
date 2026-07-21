using DataAnnotationValidationUsingMinimalAPI.Enums;
using DataAnnotationValidationUsingMinimalAPI.Validators;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DataAnnotationValidationUsingMinimalAPI.Requests
{
    public class CreateProductRequest
    {
        [Required(ErrorMessage = "This Is Name Required")]
        [StringLength(255, ErrorMessage = "Must Not Exceded 255 Letter ")]
        public string? Name { get; set; }

        [StringLength(1000,ErrorMessage = "Must Not Exceded 1000 Letter ")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "This Is SKU Required")]
        [RegularExpression(@"^PRD-\d{5}$" , ErrorMessage = "Must Be PRD-XXXXX")]
        public string? SKU { get; set; }

        [Range(0.01,double.MaxValue,ErrorMessage =" Must Be At Least 0.01")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue ,ErrorMessage = "Must Be Integer ")]
        public int StockQuantity { get; set; }
        
        [Required(ErrorMessage = "This Is LaunchDate Required")]
        [CustomValidation(typeof(LaunchDateValidators),nameof(LaunchDateValidators.MustBeTodayOrFuture))]
        public DateTime LaunchDate { get; set; }

        [EnumDataType(typeof(ProductCategory),ErrorMessage = "Invalid Product Category")]
        public ProductCategory Category { get; set; }
        [Url(ErrorMessage = "Invalid Url Images")]
        public string? ImageUrl { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = " Must Be At Least 0.01")]
        public decimal Weight { get; set; }

        [CustomValidation(typeof(WarrantyValidators),nameof(WarrantyValidators.MustBe_0_1_2_3))]
        public int WarrantyPeriodMonths { get; set; }
        public bool IsReturnable { get; set; }

        [RequiredIf("IsReturnable", true ,ErrorMessage = "Must Be Policy")]
        public string? ReturnPolicyDescription { get; set; }

        [MaxLength(5,ErrorMessage = "Must be  At Most 5 ")]
        public List<string> Tags { get; set; } = new List<string>();
    }

}
