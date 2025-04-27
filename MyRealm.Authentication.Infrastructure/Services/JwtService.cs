using Microsoft.IdentityModel.Tokens;
using MyRealm.Authentication.Application.Models;
using MyRealm.Authentication.Domain.Entities;
using MyRealm.Authentication.Domain.Models;
using MyRealm.Authentication.Domain.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AccessToken = MyRealm.Authentication.Domain.Entities.AccessToken;


namespace MyRealm.Authentication.Application.Services
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
            return new(GenerateAccessToken(user), GenerateRefreshToken());
        }

        private AccessToken GenerateAccessToken(ApiUser user)
        {
            var expiry = CalculateExpiry(Settings.AccessTokenExpiryMinutes);
           
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.SecretKey)), SecurityAlgorithms.HmacSha256);
            
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName)
            };

            var token = new JwtSecurityToken(
                issuer: Settings.Issuer,
                audience: Settings.Audience,
                expires: expiry,
                claims: claims,
                signingCredentials: signingCredentials
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new(tokenString, expiry);
        }

        private RefreshToken GenerateRefreshToken() 
        {
            var expiration = CalculateExpiry(Settings.RefreshTokenExpiryMinutes);

            var bytes = new byte[32];

            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(bytes);

            var tokenString = Convert.ToBase64String(bytes);

            return new(tokenString, expiration);
        }

		private DateTime CalculateExpiry(int minutes) => DateTime.UtcNow.AddMinutes(minutes);
	}
}
