using RefreshJWTToken.Contracts;
using RefreshJWTToken.Requests;
using Microsoft.AspNetCore.Mvc;

namespace RefreshJWTToken.Controllers
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

        [HttpPost("refresh")]
        public IActionResult RefreshToken(RefreshTokenRequest request)
        {
            // Fake Data ---> In Fact Is Fetch In Database
            var refreshTokenRecordInDateBase = new
            {
                UserId = "f5aae296-97dc-4a2b-a15a-b20ce4780d3a",
                RefreshToken = "asdf546dsf49ads4f9",
                Expires = DateTime.UtcNow.AddHours(12),
            };

            if (refreshTokenRecordInDateBase is null ||
                refreshTokenRecordInDateBase.Expires < DateTime.UtcNow ||
                request.RefreshToken != "asdf546dsf49ads4f9")
                return Problem(
                     title : "Bad Request",
                     statusCode : StatusCodes.Status400BadRequest,
                     detail : "Refresh Token Is Invalid And/Or Has Expire"
                   );

            // Fake Data ---> In Fact Is Fetch In Database
            var user = new
            {
                Id = "79410514-0136-4442-be9b-01f097c57f7a",
                FirstName = "Primary",
                LastName = "Manager",
                Email = "pm@localhost",
                Permissions = new List<string>  {
                "project:create",
                  "project:read",
                  "project:update",
                  "project:delete",
                  "project:assign_member",
                  "project:manage_budget",
                  "task:create",
                  "task:read",
                  "task:update",
                  "task:delete",
                  "task:assign_user",
                  "task:update_status"
                },
                Roles = new List<string> {
                  "ProjectManager"
                }
            };
            GenerateTokenRequest userToken = new GenerateTokenRequest
            {
                Id = user.Id,
                FirstName= user.FirstName,
                LastName= user.LastName,
                Email = user.Email,
                Roles = user.Roles,
                Permissions = user.Permissions
            };
            return Ok(tokenProvider.GenerateJwtToken(userToken));
            
        }
    }
}
