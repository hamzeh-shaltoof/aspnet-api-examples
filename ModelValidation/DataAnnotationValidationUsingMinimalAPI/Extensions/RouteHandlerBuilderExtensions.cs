using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace DataAnnotationValidationUsingMinimalAPI.Extensions
{
    public static class RouteHandlerBuilderExtensions
    {
        public static RouteHandlerBuilder Validate<T>(this RouteHandlerBuilder builder)
        {
            builder.AddEndpointFilter(async(context, next) =>
            {
                // This Is Request Of User
                var argument = context.Arguments.OfType<T>().FirstOrDefault();

                if (argument is null)
                    return Results.Problem(new ProblemDetails
                    {
                        Title = "Bad Request",
                        Type = "",
                        Status = StatusCodes.Status400BadRequest,
                        Detail = $"{nameof(T)} Is NULL"
                    });

                List<ValidationResult> validationResult = []; // To Store Errors  

                var isValid = Validator.TryValidateObject( // Return True If No Error Otherwise false And Store Errors In Parameter 3 (validationResult)  
                                            argument, // This Is Request Of User To Be Validation
                                            new ValidationContext(argument), //  Force .NET To Apply Data Annotation On  Request Of User (argument)
                                            validationResult,// To Store Errors
                                            true); // Go Apply Data Annotation

                if (!isValid)
                {

                    var errorGroups = validationResult.SelectMany(v => (v.MemberNames.Any() ? v.MemberNames : new[] { "" })
                                                      .Select(name => new { name, v.ErrorMessage }))
                                                      .GroupBy(x => x.name)
                                                      .ToDictionary(
                                                             g => g.Key,
                                                             g => g.Select(x => x.ErrorMessage).ToArray());

                    return Results.ValidationProblem(errorGroups!);
                }



                return await next(context);

            });

            return builder;

        }
    }
}
