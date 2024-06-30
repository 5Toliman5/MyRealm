using Microsoft.EntityFrameworkCore;
using MyRealm.Domain.Authentication.Entities;
using System.Reflection.Metadata;

namespace MyRealm.DataAccess.EFDbContexts
{
    public class AuthenticationDbContext : DbContext
    {
        public DbSet<ApiUser> Users { get; set; }
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        {

        }
        public AuthenticationDbContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApiUser>()
                .HasIndex(x => x.UserName)
                .IsUnique();
            modelBuilder.Entity<ApiUser>()
                .HasIndex(x => x.AccessToken)
                .IsUnique();
            modelBuilder.Entity<ApiUser>()
                .HasIndex(x => x.RefreshToken)
                .IsUnique();
        }
    }
}
