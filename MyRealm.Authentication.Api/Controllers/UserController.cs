using Microsoft.AspNetCore.Mvc;
using MyRealm.Authentication.Contracts.Request;
using MyRealm.Authentication.Domain.Services;

namespace MyRealm.Authentication.Api.Controllers
{
    public class UserController : AbstractController
	{
        private readonly IUserService UserService;

        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateApiUserRequest request)
        {
            await UserService.CreateUserAsync(new(request.UserName, request.Password, request.Email));
            return Created();
        }
    }
}
