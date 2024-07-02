namespace MyRealm.Authentication.Infrastructure.Models
{
    public record JwtSettings
    {
        public static string SectionName => nameof(JwtSettings).ToString();
        public string SecretKey { get; init; }
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public int AccessTokenExpiryMinutes { get; init; }
        public int RefreshTokenExpiryMinutes { get; init; }
    }
}
