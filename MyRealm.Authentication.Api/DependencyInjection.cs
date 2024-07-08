using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyReailm.Authentication.Domain.Repositories;
using MyReailm.Authentication.Domain.Services;
using MyRealm.Authentication.Infrastructure.Models;
using MyRealm.Authentication.Infrastructure.Services;
using MyRealm.DataAccess.EFDbContexts;
using MyRealm.DataAccess.Repositories;

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
        public static IServiceCollection RegisterInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            //builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
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
