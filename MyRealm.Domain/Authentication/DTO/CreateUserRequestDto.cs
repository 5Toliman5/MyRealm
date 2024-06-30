namespace MyRealm.Domain.Authentication.DTO
{
    public record CreateUserRequestDto
    {
        public string UserName { get; init; }
        public string Password { get; init; }
        public CreateUserRequestDto(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
