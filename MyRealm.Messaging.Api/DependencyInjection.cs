using MassTransit;
using MyRealm.Messaging.DataAccess.Repositories;
using MyRealm.Messaging.Domain.Repositories;
using MyRealm.Messaging.Infrastructure;
using MyRealm.Messaging.RabbitMQ.Client;

namespace MyRealm.Messaging.Api
{
    public static class DependencyInjection
    {
        public static void RegisterRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IRabbitMqClient, RabbitMqClient>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<RabbitMqClient>>();
                var messageScheduler = sp.GetRequiredService<IMessageScheduler>();
                var sendDelayInSeconds = configuration.GetValue<int>("RabbitMQ:SendDelayInSeconds");
                return new RabbitMqClient(logger, messageScheduler, sendDelayInSeconds);
            });
            services.AddMassTransit(x =>
            {
                x.AddDelayedMessageScheduler();
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    var url = configuration.GetValue<string>("RabbitMQ:Url");
                    var userName = configuration.GetValue<string>("RabbitMQ:UserName");
                    var password = configuration.GetValue<string>("RabbitMQ:Password");
                    cfg.Host(new Uri(url), h =>
                    {
                        h.Username(userName);
                        h.Password(password);
                    });
                    cfg.ConfigureEndpoints(ctx);
                });
            });
        }
        public static void RegisterDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddTransient<IMessageRepository>(s => new MessageRepository(connectionString));
        }
        public static void RegisterInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var messageConfiguration = new MessageConfiguration();
            configuration.Bind(MessageConfiguration.SectionName, messageConfiguration);
            services.AddSingleton(messageConfiguration);
        }
    }
}
