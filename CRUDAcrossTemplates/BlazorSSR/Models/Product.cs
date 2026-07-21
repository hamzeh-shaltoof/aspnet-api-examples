using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BlazorSSR.Models
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
