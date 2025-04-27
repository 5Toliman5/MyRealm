using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using MyRealm.Authentication.Application.Models;
using MyRealm.Authentication.Application.Services;
using MyRealm.Authentication.DataAccess.EF;
using MyRealm.Authentication.DataAccess.Repositories;
using MyRealm.Authentication.Domain.Repositories;
using MyRealm.Authentication.Domain.Services;

namespace MyRealm.Authentication.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthenticationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Default")));
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }
        public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthService>();
            return services;
        }
    }
}
