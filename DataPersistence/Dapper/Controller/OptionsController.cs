using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DapperMicroOptimizations.Controller
{
    [ApiController]
    [Route("api/options")]
    public class OptionsController : ControllerBase
    {
        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Append("Allow", "OPTIONS , HEAD , GET , POST , PUT , PATCH , DELETE");
            // Status Code 204 Is No Content 
            return NoContent(); 
        }
    }
}
