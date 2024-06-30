using MyRealm.Domain.Authentication.Entities;

namespace MyRealm.Domain.Authentication.DTO
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
