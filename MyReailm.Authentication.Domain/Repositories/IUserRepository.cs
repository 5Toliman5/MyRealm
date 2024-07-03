using MyReailm.Authentication.Domain.Entities;
using MyRealm.Common.Repositories;

namespace MyReailm.Authentication.Domain.Repositories
{
    public interface IUserRepository : IRepository<ApiUser>
    {
        Task<ApiUser?> GetByUserNameAsync(string userName);
        Task<ApiUser?> GetByAccessTokenAsync(string token);
        Task<ApiUser?> GetByResreshTokenAsync(string token);
        Task<IList<string>> GetAllUserNamesAsync();
    }
}