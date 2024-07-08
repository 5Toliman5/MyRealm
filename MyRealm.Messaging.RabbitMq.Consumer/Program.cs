using MassTransit;

namespace MyRealm.Messaging.RabbitMq.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            builder.Services.AddHttpClient();
            builder.Services.RegisterRabbitMq(builder.Configuration);
            builder.Services.RegisterDataAccessLayer(builder.Configuration);
            builder.Services.RegisterInfrastructureLayer(builder.Configuration);
            var app = builder.Build();
            app.Run();
        }
    }
}
