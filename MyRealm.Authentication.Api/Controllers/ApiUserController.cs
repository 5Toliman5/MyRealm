using Microsoft.AspNetCore.Mvc;
using MyReailm.Authentication.Domain.Services;
using MyRealm.Authentication.Contracts.Request;

namespace MyRealm.Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiUserController : BaseController
    {
        private readonly IUserService UserService;

        public ApiUserController(ILogger<BaseController> logger, IUserService userService) : base(logger)
        {
            this.UserService = userService;
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
