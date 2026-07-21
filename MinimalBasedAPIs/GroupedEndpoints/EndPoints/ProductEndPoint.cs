
using Microsoft.AspNetCore.Http.HttpResults;
using System.Xml.Linq;

namespace GroupedEndpoints.EndPoints
{
    public static class ProductEndPoint
    {
        public static RouteGroupBuilder MapProductEndPoint(this IEndpointRouteBuilder app)
        {
            var productApi = app.MapGroup("api/products");


            productApi.MapGet("/", GetAllProduct);
            productApi.MapGet("/{id:int}", GetProductById);
            productApi.MapPost("/", CreateProduct);

            return productApi;
        }
         

        private static Object GetAllProduct(HttpContext context)
        {
            return new
            {
                Product = new
                {
                    Id = 1,
                    Name = "Apple",
                    Price = 10m
                },

                Product2 = new
                {
                    Id = 2,
                    Name = "Banana",
                    Price = 5m
                }
            };
        }

        private static Object GetProductById(int id)
        {
            if (id == 1)
            {
                return new
                {
                    Product = new
                    {
                        Id = 1,
                        Name = "Apple",
                        Price = 10m
                    }
                };
            };
            if (id == 2)
            {
                return new
                {
                    Product2 = new
                    {
                        Id = 2,
                        Name = "Banana",
                        Price = 5m
                    }
                };
            }
            else
            {
                return new
                {
                    Product3 = new
                    {
                        Id = id,
                        Name = "New Product",
                        Price = 5m
                    }
                };
            }
        }

        private static IResult CreateProduct(object product)
        {
            return product is null
                ? Results.NotFound()
                : Results.Ok(product); 
        }
    }
}
