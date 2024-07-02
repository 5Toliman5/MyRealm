using Microsoft.AspNet.Identity;
using MyRealm.Authentication.Infrastructure.Exceptions;
using MyRealm.Domain.Authentication.DTO;
using MyRealm.Domain.Authentication.Entities;
using MyRealm.Domain.Authentication.Repositories;

namespace MyRealm.Authentication.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository UserRepository;
        private readonly IPasswordHasher PasswordHasher;
        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            UserRepository = userRepository;
            PasswordHasher = passwordHasher;
        }

        public async Task CreateUserAsync(CreateUserRequestDto request)
        {
            var existingUser = await this.UserRepository.GetByUserNameAsync(request.UserName);
            if (existingUser is not null)
                throw new UserAlreadyExistsException($"There is already a user with username {request.UserName}.");
            var passwordHash = this.PasswordHasher.HashPassword(request.Password);
            var user = new ApiUser(request.UserName, passwordHash);
            await this.UserRepository.InsertAsync(user);
        }
        public async Task<bool> CheckIfUsernameIsTaken(string userName)
        {
            var usernames = await this.UserRepository.GetAllUserNamesAsync();
            return userName.Contains(userName);
        }
    }
}
