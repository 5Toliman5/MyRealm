using MyRealm.Domain.Authentication.DTO;

namespace MyRealm.Domain.Authentication.Services
{
    public interface IAuthService
    {
        Task<AuthenticateUserResponseDto> AuthenticateUserAsync(AuthenticateUserRequestDto request);
        Task<AuthenticateUserResponseDto> RefreshTokensAsync(string refreshToken);
    }
}