using Microsoft.AspNetCore.Mvc;
using MyReailm.Authentication.Domain.Services;
using MyRealm.Authentication.Contracts.Request;

namespace MyRealm.Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiUserController : ControllerBase
	{
        private readonly IUserService UserService;

        public ApiUserController(IUserService userService)
        {
            this.UserService = userService;
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser(CreateApiUserRequest request)
        {
            await this.UserService.CreateUserAsync(new(request.UserName, request.Password));
            return Created();
        }
    }
}
