using MyRealm.Authentication.Domain.Entities;
using MyRealm.Common.Repositories.EF;

namespace MyRealm.Authentication.Domain.Repositories
{
    public interface IUserRepository : IRepository<ApiUser, int>
    {
        Task<ApiUser?> GetByUserNameAsync(string userName);

        Task<ApiUser?> GetByAccessTokenAsync(string token);

        Task<ApiUser?> GetByResreshTokenAsync(string token);

        Task<bool> CheckIfUserNameIsTaken(string userName);
    }
}