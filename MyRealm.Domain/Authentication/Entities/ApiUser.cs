using MyRealm.Domain.Common.Entities;

namespace MyRealm.Domain.Authentication.Entities
{
    public class ApiUser : BaseEntity
    {
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
            this.AccessToken = token.Value;
            this.AccessTokenExpiry = token.Expiry;
        }
        public void SetRefreshToken(SecurityToken token)
        {
            this.RefreshToken = token.Value;
            this.RefreshTokenExpiry = token.Expiry;
        }
    }
}
