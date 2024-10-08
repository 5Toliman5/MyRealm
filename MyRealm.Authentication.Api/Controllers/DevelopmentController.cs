using Microsoft.AspNetCore.Mvc;
using MyReailm.Authentication.Domain.Entities;
using MyReailm.Authentication.Domain.Repositories;

namespace MyRealm.Authentication.Api.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class DevelopmentController : ControllerBase
	{
        private readonly IUserRepository UserRepository;
        public DevelopmentController(IUserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }

        [HttpGet]
        public async Task<IList<ApiUser>> Get()
        {
            return await this.UserRepository.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<ApiUser?> Get(int id)
        {
            return await this.UserRepository.GetByIdAsync(id);
        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await this.UserRepository.DeleteByIdAsync(id);
        }
    }
}
