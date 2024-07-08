using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using MyRealm.Messaging.DataAccess.Repositories;
using MyRealm.Messaging.Domain.Repositories;
using MyRealm.Messaging.Domain.Services.Notifications;
using MyRealm.Messaging.Infrastructure.Services.Notifications;
using MyRealm.Messaging.RabbitMq.Consumer.Consumers;

namespace MyRealm.Messaging.RabbitMq.Consumer
{
    public static class DepedencyInjection
    {
        public static void RegisterRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddDelayedMessageScheduler();
                x.AddConsumer<EmailNotificationConsumer>();
                x.AddConsumer<SmsNotificationConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitMqUrl = configuration.GetValue<string>("RabbitMqConnectionString");
                    cfg.Host(rabbitMqUrl);
                    cfg.ConfigureEndpoints(context);
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
            services.AddTransient<ISmsService, SmsService>();
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
