using MyReailm.Authentication.Domain.DTO;
using MyReailm.Authentication.Domain.Entities;

namespace MyReailm.Authentication.Domain.Services
{
    public interface IJwtService
    {
        AuthenticateUserResponseDto GenerateTokens(ApiUser user);
    }
}
