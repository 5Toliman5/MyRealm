using MyRealm.Authentication.Domain.Models;

namespace MyRealm.Authentication.Domain.Services
{
    public interface IAuthService
    {
        Task<AuthenticateUserResponseDto> AuthenticateUserAsync(AuthenticateUserRequestDto request);

        Task<AuthenticateUserResponseDto> RefreshTokensAsync(string refreshToken);
    }
}