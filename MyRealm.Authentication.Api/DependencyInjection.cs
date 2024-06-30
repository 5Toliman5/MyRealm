﻿using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyRealm.Authentication.Infrastructure.Models;
using MyRealm.Authentication.Infrastructure.Services;
using MyRealm.DataAccess.EFDbContexts;
using MyRealm.DataAccess.Repositories;
using MyRealm.Domain.Authentication.Repositories;
using MyRealm.Domain.Authentication.Services;

namespace MyRealm.Authentication.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthenticationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Default")));
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
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
