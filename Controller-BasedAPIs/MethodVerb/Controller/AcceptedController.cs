using Microsoft.AspNetCore.Mvc;

namespace MethodVerb.Controller
{
    [ApiController]
    [Route("/api/Accepted")]
    public class AcceptedController : ControllerBase 
    {
        [HttpGet]
         public IActionResult Accepted()
        {
            var id = Guid.NewGuid();

            return Accepted($"/api/Accepted/status/{id}",new { Id = id, StatusCode = "Processing" });
        }
        [HttpGet("status/{id:guid}")]

        public IActionResult GetStatusProcessing(Guid id)
        {
           var isStillProcessing = false; // fake it 
            return Ok(new { id, status = isStillProcessing ? "Processing" : "Completed" });
        }
    }
}
