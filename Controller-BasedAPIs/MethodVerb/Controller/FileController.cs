using MethodVerb.Data;
using MethodVerb.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace MethodVerb.Controller
{
    [ApiController]
    [Route("api/file")]
    public class FileController(ProductRepository repository) : ControllerBase
    {
        [HttpGet("csv")]
        public IActionResult FileCSV()
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Id,Name,Price");

            var products = repository.Products;

            foreach(var product in products)
               {
                csvBuilder.AppendLine($"{product.Id},{product.Name},{product.Price}"); 
               }

            var fileByte = Encoding.UTF8.GetBytes(csvBuilder.ToString());

            return File(fileByte, "text/csv", "productCSV_1_100_.csv");
        }
        [HttpGet("FilePhysical")]
        public IActionResult FilePhysical()
        {
            string filePhysical = Path.Combine(Directory.GetCurrentDirectory(), "Files", "products.csv");
            return PhysicalFile(filePhysical, "text/csv", "AllProducts.csv");
        }
    }
}
