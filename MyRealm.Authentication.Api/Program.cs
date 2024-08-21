using Microsoft.EntityFrameworkCore;
using MyRealm.Common.Middleware;
using MyRealm.DataAccess.EFDbContexts;

namespace MyRealm.Authentication.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.RegisterDataAccessLayer(builder.Configuration);
            builder.Services.RegisterInfrastructureLayer(builder.Configuration);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            using var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();
            context.Database.Migrate();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
