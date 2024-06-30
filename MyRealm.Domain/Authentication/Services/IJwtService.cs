using MyRealm.Domain.Authentication.DTO;
using MyRealm.Domain.Authentication.Entities;

namespace MyRealm.Domain.Authentication.Services
{
    public interface IJwtService
    {
        AuthenticateUserResponseDto GenerateTokens(ApiUser user);
    }
}
