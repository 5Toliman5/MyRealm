using Microsoft.EntityFrameworkCore;
using MyRealm.Authentication.DataAccess.EF;
using MyRealm.Authentication.Domain.Entities;
using MyRealm.Authentication.Domain.Repositories;
using MyRealm.Common.Repositories.EF;

namespace MyRealm.Authentication.DataAccess.Repositories
{
    public class UserRepository : DbContextRepository<AuthenticationDbContext, ApiUser, int>, IUserRepository
    {
        public UserRepository(AuthenticationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<ApiUser?> GetByAccessTokenAsync(string token)
        {
            return Context.Users.SingleOrDefaultAsync(x => x.AccessToken.Value == token);
        }

        public Task<ApiUser?> GetByResreshTokenAsync(string token)
        {
            return Context.Users.SingleOrDefaultAsync(x => x.RefreshToken.Value == token);
        }

        public Task<ApiUser?> GetByUserNameAsync(string userName)
        {
            return Context.Users.SingleOrDefaultAsync(x => x.UserName == userName);
        }

		public Task<bool> CheckIfUserNameIsTaken(string userName)
        {
			return Context.Users.AnyAsync(x => x.UserName == userName);
		}
	}
}
