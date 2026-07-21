using System.ComponentModel.DataAnnotations;

namespace M01.ModelAndInMemoryStoreSetupMVC.Model
{
    public class Product
    {
        public Guid Id { get; set; }

        [Display(Name = "Product Name")]
        public string? Name { get; set; }

        [Display(Name = "Product Price")]
        public decimal Price  { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
    }
}
