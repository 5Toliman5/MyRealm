namespace MyRealm.Contracts.Authentication.Request
{
    public record RefreshRequest
    {
        public string RefreshToken { get; init; }

        public RefreshRequest(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
