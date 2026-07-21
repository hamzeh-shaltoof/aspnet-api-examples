using Microsoft.AspNetCore.Mvc.Filters;

namespace ImplementingActionFilters.Filters
{
    public class SampleFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Sample Sync Before");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Sample Sync After");
        }

        
    }
}
