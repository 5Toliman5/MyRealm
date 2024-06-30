using MyRealm.Domain.Authentication.DTO;

namespace MyRealm.Authentication.Infrastructure.Services
{
    public interface IUserService
    {
        Task<bool> CheckIfUsernameIsTaken(string userName);
        Task CreateUserAsync(CreateUserRequestDto request);
    }
}