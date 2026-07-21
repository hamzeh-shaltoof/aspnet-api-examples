using ImplementTokenGenerationLogic.Requests;
using ImplementTokenGenerationLogic.Responses;

namespace ImplementTokenGenerationLogic.Contracts
{
    public interface IJwtTokenProvider
    {
        public TokenResponse GenerateJwtToken(GenerateTokenRequest request);
    }
}
