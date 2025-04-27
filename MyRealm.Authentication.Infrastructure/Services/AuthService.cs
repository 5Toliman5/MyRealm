using Microsoft.AspNet.Identity;
using MyRealm.Authentication.Domain.Exceptions;
using MyRealm.Authentication.Domain.Entities;
using MyRealm.Authentication.Domain.Models;
using MyRealm.Authentication.Domain.Repositories;
using MyRealm.Authentication.Domain.Services;
using MyRealm.Common.Exceptions;

namespace MyRealm.Authentication.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher PasswordHasher;
        private readonly IUserRepository UserRepository;
        private readonly IJwtService JwtService;

        public AuthService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtService jwtService)
        {
            PasswordHasher = passwordHasher;
            UserRepository = userRepository;
            JwtService = jwtService;
        }
        public async Task<AuthenticateUserResponseDto> AuthenticateUserAsync(AuthenticateUserRequestDto request)
        {
            var user = await UserRepository.GetByUserNameAsync(request.UserName);
            if (user is null)
                throw new NotFoundException($"User {request.UserName} does not exist.");
            if (PasswordHasher.VerifyHashedPassword(user.Password, request.Password) == PasswordVerificationResult.Failed)
                throw new WrongPasswordException($"Wrong password.");
            return await CreateTokensAsync(user);
        }

        public async Task<AuthenticateUserResponseDto> RefreshTokensAsync(string refreshToken)
        {
            var user = await UserRepository.GetByResreshTokenAsync(refreshToken);
            if (user is null)
                throw new NotFoundException($"No user possesses the provided refresh token.");
            return await CreateTokensAsync(user);
        }
        private async Task<AuthenticateUserResponseDto> CreateTokensAsync(ApiUser user)
        {
            var tokens = JwtService.GenerateTokens(user);
            user.AccessToken = tokens.AccessToken;
            user.RefreshToken = tokens.RefreshToken;
            await UserRepository.UpdateAsync(user);
            return tokens;
        }
    }
}
