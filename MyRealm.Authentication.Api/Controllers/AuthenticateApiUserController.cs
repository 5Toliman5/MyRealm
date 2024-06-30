using Microsoft.AspNetCore.Mvc;
using MyRealm.Contracts.Authentication.Request;
using MyRealm.Contracts.Authentication.Response;
using MyRealm.Domain.Authentication.Entities;
using MyRealm.Domain.Authentication.Services;
using MyRealm.Domain.Common.Repositories;

namespace MyRealm.Authentication.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateApiUserController : ControllerBase
    {
        private readonly ILogger<AuthenticateApiUserController> Logger;
        private readonly IAuthService AuthService;

        public AuthenticateApiUserController(ILogger<AuthenticateApiUserController> logger, IAuthService authService)
        {
            this.Logger = logger;
            this.AuthService = authService;
        }

        [HttpPost("Authenticate")]
        public async Task<AuthenticateApiUserResponse> Authenticate(AuthenticateApiUserRequest request)
        {
            this.Logger.LogInformation($"Start authenticating user: {request.UserName}");
            var result = await this.AuthService.AuthenticateUserAsync(new(request.UserName, request.Password));
            return new(result.AccessToken.Value, result.RefreshToken.Value);
        }
        [HttpPost("Refresh")]
        public async Task<AuthenticateApiUserResponse> Refresh(RefreshRequest request)
        {
            var result = await this.AuthService.RefreshTokensAsync(request.RefreshToken);
            return new(result.AccessToken.Value, result.RefreshToken.Value);
        }
    }
}
