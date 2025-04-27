using Microsoft.AspNetCore.Mvc;
using MyRealm.Authentication.Contracts.Request;
using MyRealm.Authentication.Contracts.Response;
using MyRealm.Authentication.Domain.Services;

namespace MyRealm.Authentication.Api.Controllers
{
    public class AuthController : AbstractController
	{
        private readonly IAuthService AuthService;

        public AuthController(IAuthService authService)
        {
            AuthService = authService;
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateApiUserRequest request)
        {
            var result = await AuthService.AuthenticateUserAsync(new(request.UserName, request.Password));
            var response = new AuthenticateApiUserResponse(result.AccessToken.Value, result.RefreshToken.Value);
			return Ok(response);
        }
        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh(RefreshRequest request)
        {
            var result = await AuthService.RefreshTokensAsync(request.RefreshToken);
			var response = new AuthenticateApiUserResponse(result.AccessToken.Value, result.RefreshToken.Value);
			return Ok(response);
		}
    }
}
