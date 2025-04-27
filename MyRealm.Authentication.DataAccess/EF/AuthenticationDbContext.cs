using Microsoft.EntityFrameworkCore;
using MyRealm.Authentication.Domain.Entities;

namespace MyRealm.Authentication.DataAccess.EF
{
    public class AuthenticationDbContext : DbContext
    {
        public DbSet<ApiUser> Users { get; set; }

		public DbSet<AccessToken> SecurityTokens { get; set; }

		public DbSet<RefreshToken> RefreshTokens { get; set; }

		public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<ApiUser>()
				.HasOne(apiUser => apiUser.AccessToken)
				.WithOne()
				.HasForeignKey<AccessToken>(x => x.UserId);

			modelBuilder.Entity<ApiUser>()
				.HasOne(apiUser => apiUser.RefreshToken)
				.WithOne()
				.HasForeignKey<RefreshToken>(x => x.UserId);

			modelBuilder.Entity<ApiUser>()
                .Navigation(e => e.AccessToken)
                .AutoInclude();

			modelBuilder.Entity<ApiUser>()
	            .Navigation(e => e.RefreshToken)
	            .AutoInclude();
		}
    }
}
