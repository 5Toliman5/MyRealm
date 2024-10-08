namespace MyRealm.Authentication.Contracts.Response
{
    public record AuthenticateApiUserResponse(string AccessToken, string RefreshToken);
}
