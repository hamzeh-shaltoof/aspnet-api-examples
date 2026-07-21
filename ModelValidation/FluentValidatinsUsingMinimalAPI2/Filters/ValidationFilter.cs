
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace FluentValidatinsUsingMinimalAPI.Filters
{
    public class ValidationFilter<T> : IEndpointFilter where T : class
    {
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {

            var validate = context.HttpContext.RequestServices.GetService<IValidator<T>>();

            var model = context.Arguments.OfType<T>().FirstOrDefault();

            if(validate is null ||  model == null)
                return Results.Problem(new ProblemDetails
                {
                    Title = "Bad Request",
                    Type = "",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = $"{nameof(T)} Is NULL"
                });

            var result = await Validator.ValidateAsync(model);
            return await next(context);
        }
    }
}
