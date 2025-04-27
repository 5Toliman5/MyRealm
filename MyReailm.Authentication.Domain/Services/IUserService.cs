using MyRealm.Authentication.Domain.Models;

namespace MyRealm.Authentication.Domain.Services
{
    public interface IUserService
    {
        Task<bool> CheckIfUsernameIsTaken(string userName);

        Task CreateUserAsync(CreateUserRequestDto request);
    }
}