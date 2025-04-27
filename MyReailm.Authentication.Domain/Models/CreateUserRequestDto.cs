namespace MyRealm.Authentication.Domain.Models
{
    public record CreateUserRequestDto(string UserName, string Password, string Email);

}
