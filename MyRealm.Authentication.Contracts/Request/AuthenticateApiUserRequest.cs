namespace MyRealm.Authentication.Contracts.Request
{
    public record AuthenticateApiUserRequest
    {
        public string UserName { get; init; }
        public string Password { get; init; }
        public AuthenticateApiUserRequest(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
