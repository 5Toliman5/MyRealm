using MyRealm.Authentication.Domain.Entities;
using MyRealm.Authentication.Domain.Models;

namespace MyRealm.Authentication.Domain.Services
{
    public interface IJwtService
    {
        AuthenticateUserResponseDto GenerateTokens(ApiUser user);
    }
}
