using MyReailm.Authentication.Domain.DTO;

namespace MyReailm.Authentication.Domain.Services
{
    public interface IAuthService
    {
        Task<AuthenticateUserResponseDto> AuthenticateUserAsync(AuthenticateUserRequestDto request);
        Task<AuthenticateUserResponseDto> RefreshTokensAsync(string refreshToken);
    }
}