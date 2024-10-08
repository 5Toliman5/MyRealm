using Microsoft.AspNetCore.Mvc;
using MyReailm.Authentication.Domain.Services;
using MyRealm.Authentication.Contracts.Request;
using MyRealm.Authentication.Contracts.Response;

namespace MyRealm.Authentication.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateApiUserController : ControllerBase
	{
        private readonly IAuthService AuthService;

        public AuthenticateApiUserController(IAuthService authService)
        {
            this.AuthService = authService;
        }

        [HttpPost("Authenticate")]
        public async Task<AuthenticateApiUserResponse> Authenticate(AuthenticateApiUserRequest request)
        {
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
