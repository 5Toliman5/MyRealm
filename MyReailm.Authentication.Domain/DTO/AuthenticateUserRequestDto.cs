namespace MyReailm.Authentication.Domain.DTO
{
    public record AuthenticateUserRequestDto
    {
        public string UserName { get; init; }
        public string Password { get; init; }
        public AuthenticateUserRequestDto(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
