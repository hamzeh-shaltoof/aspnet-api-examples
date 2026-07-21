using ImplementTokenGenerationLogic.Contracts;
using ImplementTokenGenerationLogic.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ImplementTokenGenerationLogic.Controllers
{
    [ApiController]
    [Route("api/token")]
    public class TokenController(IJwtTokenProvider tokenProvider) : ControllerBase
    {
        [HttpPost("generate")]
        public IActionResult GenerateToken(GenerateTokenRequest request)
        {
            return Ok(tokenProvider.GenerateJwtToken(request));
        }
    }
}
