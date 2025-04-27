using Microsoft.AspNet.Identity;
using MyRealm.Authentication.Domain.Entities;
using MyRealm.Authentication.Domain.Exceptions;
using MyRealm.Authentication.Domain.Models;
using MyRealm.Authentication.Domain.Repositories;
using MyRealm.Authentication.Domain.Services;

namespace MyRealm.Authentication.Application.Services
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
            var existingUser = await UserRepository.GetByUserNameAsync(request.UserName);
            if (existingUser is not null)
                throw new UserAlreadyExistsException($"There is already a user with username {request.UserName}.");

            var passwordHash = PasswordHasher.HashPassword(request.Password);
            var user = new ApiUser(request.UserName, passwordHash, request.Email);

            await UserRepository.InsertAsync(user);
        }

        public Task<bool> CheckIfUsernameIsTaken(string userName)
        {
			return UserRepository.CheckIfUserNameIsTaken(userName);
        }
    }
}