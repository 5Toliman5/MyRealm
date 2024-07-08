using MyReailm.Authentication.Domain.DTO;

namespace MyReailm.Authentication.Domain.Services
{
    public interface IUserService
    {
        Task<bool> CheckIfUsernameIsTaken(string userName);
        Task CreateUserAsync(CreateUserRequestDto request);
    }
}