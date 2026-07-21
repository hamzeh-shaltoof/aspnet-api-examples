using Microsoft.IdentityModel.Tokens;
using RefreshJWTToken.Contracts;
using RefreshJWTToken.Requests;
using RefreshJWTToken.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RefreshJWTToken.Services
{
    public class JwtTokenProvider(IConfiguration configuration) : IJwtTokenProvider
    {
        public TokenResponse GenerateJwtToken(GenerateTokenRequest request)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expire = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["TokenExpirationInMinutes"]!));
            var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

            var claims = new List<Claim>()
            {
                new (JwtRegisteredClaimNames.Sub,request.Id),
                new (JwtRegisteredClaimNames.FamilyName,request.LastName),
                new (JwtRegisteredClaimNames.GivenName,request.FirstName),
                new (JwtRegisteredClaimNames.Email,request.Email),
            };

            foreach (string role in request.Roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            foreach (string permission in request.Permissions)
                claims.Add(new Claim("permission", permission));

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = issuer,
                Audience = audience,
                Expires = expire,
                SigningCredentials = new SigningCredentials(
                   new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);

            return new TokenResponse()
            {
                AccessToken = tokenHandler.WriteToken(token),
                RefreshToken = "asdf546dsf49ads4f9",
                Expires = expire,
            };
            return default;

        }
    }
}
