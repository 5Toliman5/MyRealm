using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRealm.Authentication.Infrastructure.Services;
using MyRealm.Contracts.Authentication.Request;
using MyRealm.Contracts.Authentication.Response;

namespace MyRealm.Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiUserController : ControllerBase
    {
        private readonly ILogger<ApiUserController> Logger;
        private readonly IUserService UserService;

        public ApiUserController(IUserService userService, ILogger<ApiUserController> logger)
        {
            this.UserService = userService;
            this.Logger = logger;
        }
        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser(CreateApiUserRequest request)
        {
            this.Logger.LogInformation($"Start creating user: {request.UserName}");
            await this.UserService.CreateUserAsync(new(request.UserName, request.Password));
            return Created();
        }
    }
}
