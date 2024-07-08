namespace MyRealm.Authentication.Contracts.Request
{
    public record CreateApiUserRequest
    {
        public string UserName { get; init; }
        public string Password { get; init; }
        public CreateApiUserRequest(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
