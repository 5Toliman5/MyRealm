using Microsoft.AspNetCore.Mvc.Formatters;
using MyRealm.Messaging.Api.Middleware;

namespace MyRealm.Messaging.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
            });
            builder.Services.RegisterRabbitMq(builder.Configuration);
            builder.Services.RegisterDataAccessLayer(builder.Configuration);
            builder.Services.RegisterInfrastructureLayer(builder.Configuration);
            builder.Services.RegisterAnthentication(builder.Configuration);

            var app = builder.Build();
            app.UseMiddleware<MessagingApiErrorHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
