using MyRealm.Authentication.Domain.Entities;

namespace MyRealm.Authentication.Domain.Models
{
    public record AuthenticateUserResponseDto(AccessToken AccessToken, RefreshToken RefreshToken);

}
