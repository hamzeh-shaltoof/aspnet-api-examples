using RefreshJWTToken.Requests;
using RefreshJWTToken.Responses;

namespace RefreshJWTToken.Contracts
{
    public interface IJwtTokenProvider
    {
        public TokenResponse GenerateJwtToken(GenerateTokenRequest request);
    }
}
