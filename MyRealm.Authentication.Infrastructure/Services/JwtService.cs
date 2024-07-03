using Microsoft.IdentityModel.Tokens;
using MyReailm.Authentication.Domain.DTO;
using MyReailm.Authentication.Domain.Entities;
using MyReailm.Authentication.Domain.Services;
using MyRealm.Authentication.Infrastructure.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using SecurityToken = MyReailm.Authentication.Domain.Entities.SecurityToken;


namespace MyRealm.Authentication.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        public JwtSettings Settings { get; set; }
        public JwtService(JwtSettings settings)
        {
            Settings = settings;
        }
        public AuthenticateUserResponseDto GenerateTokens(ApiUser user)
        {
            return new(this.GenerateAccessToken(user), GenerateRefreshToken());
        }
        private DateTime CalculateExpiry(int minutes) => DateTime.UtcNow.AddMinutes(minutes);
        private SecurityToken GenerateAccessToken(ApiUser user)
        {
            var expiration = CalculateExpiry(this.Settings.AccessTokenExpiryMinutes);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Settings.SecretKey)), SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName)
            };
            var token = new JwtSecurityToken(
                issuer: this.Settings.Issuer,
                audience: this.Settings.Audience,
                expires: expiration,
                claims: claims,
                signingCredentials: signingCredentials
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new(tokenString, expiration);
        }
        private SecurityToken GenerateRefreshToken() 
        {
            var expiration = CalculateExpiry(this.Settings.RefreshTokenExpiryMinutes);
            var bytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            var tokenString = Convert.ToBase64String(bytes);
            return new(tokenString, expiration);
        }
    }
}
