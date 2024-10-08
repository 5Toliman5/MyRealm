using MyReailm.Authentication.Domain.Entities;

namespace MyReailm.Authentication.Domain.DTO
{
    public record AuthenticateUserResponseDto(SecurityToken AccessToken, SecurityToken RefreshToken);

}
