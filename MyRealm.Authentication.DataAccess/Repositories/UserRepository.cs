using Microsoft.EntityFrameworkCore;
using MyReailm.Authentication.Domain.Entities;
using MyReailm.Authentication.Domain.Repositories;
using MyRealm.Common.Repositories;
using MyRealm.DataAccess.EFDbContexts;

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
