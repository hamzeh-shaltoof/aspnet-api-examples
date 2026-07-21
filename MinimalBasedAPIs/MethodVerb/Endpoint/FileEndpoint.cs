using RepositoryPattern.Data;
using RepositoryPattern.Interface;
using RepositoryPattern.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace RepositoryPattern.Endpoint
{
    public static class FileEndpoint
    {

        public static RouteGroupBuilder MapFileGroup(this IEndpointRouteBuilder app)
        {

            var AcceptApi = app.MapGroup("api/file");
            AcceptApi.MapGet("csv", FileCSV);
            AcceptApi.MapGet("FilePhysical", FilePhysical);

            return AcceptApi;
        }
        public static IResult FileCSV(IProductRepository repository)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Id,Name,Price");

            var products = repository.GetAllProducts();

            foreach (var product in products)
            {
                csvBuilder.AppendLine($"{product.Id},{product.Name},{product.Price}");
            }

            var fileByte = Encoding.UTF8.GetBytes(csvBuilder.ToString());

            return Results.File(fileByte, "text/csv", "productCSV_1_100_.csv");
        }


        public static IResult FilePhysical()
        {
            string filePhysical = Path.Combine(Directory.GetCurrentDirectory(), "Files", "products.csv");
            return Results.File(filePhysical, "text/csv", "AllProducts.csv");
        }
    }
        

}
