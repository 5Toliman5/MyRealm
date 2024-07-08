using MyRealm.Common.Entities;

namespace MyReailm.Authentication.Domain.Entities
{
    public class ApiUser : IIntegerIdEntity
    {
        public int Id { get; init; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; init; }
        public string? AccessToken { get; private set; }
        public DateTime? AccessTokenExpiry { get; private set; }
        public string? RefreshToken { get; private set; }
        public DateTime? RefreshTokenExpiry { get; private set; }

        public ApiUser(string userName, string password)
        {
            UserName = userName;
            Password = password;
            CreatedAt = DateTime.UtcNow;
        }
        protected ApiUser()
        {

        }
        public void SetAccessToken(SecurityToken token)
        {
            AccessToken = token.Value;
            AccessTokenExpiry = token.Expiry;
        }
        public void SetRefreshToken(SecurityToken token)
        {
            RefreshToken = token.Value;
            RefreshTokenExpiry = token.Expiry;
        }
    }
}
