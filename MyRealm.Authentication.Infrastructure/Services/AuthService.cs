using Microsoft.AspNet.Identity;
using MyReailm.Authentication.Domain.DTO;
using MyReailm.Authentication.Domain.Entities;
using MyReailm.Authentication.Domain.Repositories;
using MyReailm.Authentication.Domain.Services;
using MyRealm.Authentication.Infrastructure.Exceptions;
using MyRealm.Common.Exceptions;

namespace MyRealm.Authentication.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher PasswordHasher;
        private readonly IUserRepository UserRepository;
        private readonly IJwtService JwtService;

        public AuthService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtService jwtService)
        {
            this.PasswordHasher = passwordHasher;
            this.UserRepository = userRepository;
            this.JwtService = jwtService;
        }
        public async Task<AuthenticateUserResponseDto> AuthenticateUserAsync(AuthenticateUserRequestDto request)
        {
            var user = await this.UserRepository.GetByUserNameAsync(request.UserName);
            if (user is null)
                throw new NotFoundException($"User {request.UserName} does not exist.");
            if (this.PasswordHasher.VerifyHashedPassword(user.Password, request.Password) == PasswordVerificationResult.Failed)
                throw new WrongPasswordException($"Wrong password.");
            return await CreateTokensAsync(user);
        }

        public async Task<AuthenticateUserResponseDto> RefreshTokensAsync(string refreshToken)
        {
            var user = await this.UserRepository.GetByResreshTokenAsync(refreshToken);
            if (user is null)
                throw new NotFoundException($"No user possesses the provided refresh token.");
            return await CreateTokensAsync(user);
        }
        private async Task<AuthenticateUserResponseDto> CreateTokensAsync(ApiUser user)
        {
            var tokens = this.JwtService.GenerateTokens(user);
            user.SetAccessToken(tokens.AccessToken);
            user.SetRefreshToken(tokens.RefreshToken);
            await this.UserRepository.UpdateAsync(user);
            return tokens;
        }
    }
}
