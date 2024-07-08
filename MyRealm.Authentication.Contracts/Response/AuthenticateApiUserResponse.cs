namespace MyRealm.Authentication.Contracts.Response
{
    public record AuthenticateApiUserResponse
    {
        public string AccessToken { get; init; }
        public string RefreshToken { get; init; }
        public AuthenticateApiUserResponse(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
