using ImplementTokenGenerationLogic.Contracts;
using ImplementTokenGenerationLogic.Requests;
using ImplementTokenGenerationLogic.Responses;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ImplementTokenGenerationLogic.Services
{
    public class JwtTokenProvider(IConfiguration configuration) : IJwtTokenProvider
    {
        public TokenResponse GenerateJwtToken(GenerateTokenRequest request)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expiers = DateTime.UtcNow
                .AddMinutes(int.Parse(jwtSettings["TokenExpirationInMinutes"]!));
            var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

            List<Claim> claims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Sub,request.Id),
                new(JwtRegisteredClaimNames.FamilyName,request.LastName),
                new(JwtRegisteredClaimNames.GivenName,request.FirstName),
                new(JwtRegisteredClaimNames.Email,request.Email)
            };
            foreach (string role in request.Roles)
                claims.Add(new(ClaimTypes.Role, role));

            foreach(string permission in request.Permissions)
                claims.Add(new("permission", permission));

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature 
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(descriptor);

            return new TokenResponse{
                AccessToken = tokenHandler.WriteToken(securityToken),
                RefreshToken = "asdf546dsf49ads4f9",
                Expires = expiers
            }
            ;


        }
    }
}
