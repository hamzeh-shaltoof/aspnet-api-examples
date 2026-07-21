using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperExceptionPage.Contollers
{
    public class ErrorContoller : ControllerBase
    {
        [Route("error")]
        public IActionResult ErrorProduction()
        {
            var problemDetails = new
            {
                Type = "https://example.com/probs/internal-server-error",
                Title = "Internal Server Error",
                Status = StatusCodes.Status500InternalServerError,
                Details = "An Unexpected Error Occurred",
                Instance = HttpContext.Request.Path
            };

            return new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status,
            };
        }

        [Route("error-development")] 
        public IActionResult ErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
        {
          if(!hostEnvironment.IsDevelopment())
                return NotFound();

         else
         {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()!.Error!;
           
             var problemDetails = new
             {
                 Type = "https://example.com/probs/internal-server-error",
                 Title = exception.Message?? "UnHandle Exception",
                 StatusCode = StatusCodes.Status500InternalServerError,
                 Details = exception.StackTrace,
                 Instance = HttpContext.Request.Path
             };

                return new ObjectResult(problemDetails)
                {
                    StatusCode = problemDetails.StatusCode,  
                };
         }
        
        }
    }
}
