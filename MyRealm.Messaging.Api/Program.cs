
using MassTransit;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using MyRealm.Messaging.Api.Middleware;
using MyRealm.Messaging.DataAccess.Repositories;
using MyRealm.Messaging.Domain.Repositories;
using MyRealm.Messaging.RabbitMQ.Client;

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


            var app = builder.Build();
            app.UseMiddleware<MessagingApiErrorHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
