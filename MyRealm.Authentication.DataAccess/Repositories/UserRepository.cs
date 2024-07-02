using Microsoft.EntityFrameworkCore;
using MyRealm.DataAccess.EFDbContexts;
using MyRealm.Domain.Authentication.Entities;
using MyRealm.Domain.Authentication.Repositories;
using MyRealm.Domain.Common.Repositories;

namespace MyRealm.DataAccess.Repositories
{
    public class UserRepository : DbContextRepository<AuthenticationDbContext, ApiUser>, IUserRepository
    {
        public UserRepository(AuthenticationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IList<string>> GetAllUserNamesAsync()
        {
            return await this.DbContext.Users.Select(u => u.UserName).ToListAsync();
        }

        public async Task<ApiUser?> GetByAccessTokenAsync(string token)
        {
            return await this.DbContext.Users.SingleOrDefaultAsync(x => x.AccessToken == token);
        }

        public async Task<ApiUser?> GetByResreshTokenAsync(string token)
        {
            return await this.DbContext.Users.SingleOrDefaultAsync(x => x.RefreshToken == token);
        }

        public async Task<ApiUser?> GetByUserNameAsync(string userName)
        {
            return await this.DbContext.Users.SingleOrDefaultAsync(x => x.UserName == userName);
        }
    }
}
