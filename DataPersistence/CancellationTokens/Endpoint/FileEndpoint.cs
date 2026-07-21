using CancellationTokens.Data;
using CancellationTokens.Interface;
using CancellationTokens.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CancellationTokens.Endpoint
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
        public static async Task <IResult> FileCSV(IProductRepository repository, CancellationToken cancellationToken)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Id,Name,Price");

            var products = await repository.GetAllProductsAsync(cancellationToken);

            foreach (var product in products)
            {
                csvBuilder.AppendLine($"{product.Id},{product.Name},{product.Price}");
            }

            var fileByte = Encoding.UTF8.GetBytes(csvBuilder.ToString());

            return  Results.File(fileByte, "text/csv", "productCSV_1_100_.csv");
        }



        public static IResult FilePhysical(CancellationToken cancellationToken)
        {
            string filePhysical = Path.Combine(Directory.GetCurrentDirectory(), "Files", "products.csv");

            var fileStream = File.OpenRead(filePhysical);

            return Results.File(fileStream, "text/csv", "AllProducts.csv");
        }
    }
        

}
