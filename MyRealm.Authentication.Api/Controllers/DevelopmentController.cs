using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRealm.Authentication.Infrastructure.Services;
using MyRealm.Contracts.Authentication.Request;
using MyRealm.Domain.Authentication.Entities;
using MyRealm.Domain.Authentication.Repositories;

namespace MyRealm.Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopmentController : BaseController
    {
        private readonly IUserRepository UserRepository;
        public DevelopmentController(ILogger<BaseController> logger, IUserRepository userRepository) : base(logger)
        {
            this.UserRepository = userRepository;
        }

        [HttpGet]
        public async Task<IList<ApiUser>> Get()
        {
            this.Logger.LogInformation($"Start getting users");
            return await this.UserRepository.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<ApiUser?> Get(int id)
        {
            this.Logger.LogInformation($"Start getting user: {id}");
            return await this.UserRepository.GetByIdAsync(id);
        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            this.Logger.LogInformation($"Start deleting user: {id}");
            await this.UserRepository.DeleteByIdAsync(id);
        }
    }
}
