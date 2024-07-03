using MyReailm.Authentication.Domain.Entities;

namespace MyReailm.Authentication.Domain.DTO
{
    public record AuthenticateUserResponseDto
    {
        public SecurityToken AccessToken { get; init; }
        public SecurityToken RefreshToken { get; init; }
        public AuthenticateUserResponseDto(SecurityToken accessToken, SecurityToken refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
