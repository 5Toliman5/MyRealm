using MyRealm.Domain.Authentication.Entities;
using MyRealm.Domain.Common.Repositories;

namespace MyRealm.Domain.Authentication.Repositories
{
    public interface IUserRepository : IRepository<ApiUser>
    {
        Task<ApiUser?> GetByUserNameAsync(string userName);
        Task<ApiUser?> GetByAccessTokenAsync(string token);
        Task<ApiUser?> GetByResreshTokenAsync(string token);
        Task<IList<string>> GetAllUserNamesAsync();
    }
}