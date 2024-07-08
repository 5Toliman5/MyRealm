using MassTransit;
using MyRealm.Messaging.Domain.Entities.Domain.Notifications;
using MyRealm.Messaging.Domain.Entities.RabbitMq.Notifications;
using MyRealm.Messaging.Domain.Repositories;
using MyRealm.Messaging.Domain.Services.Notifications;

namespace MyRealm.Messaging.RabbitMq.Consumer.Consumers
{
    public class EmailNotificationConsumer : BaseNotificationsConsumer<EmailNotificationRabbitMqMessage>
    {
        private readonly IEmailService EmailService;

        public EmailNotificationConsumer(IConfiguration configuration, ILogger<BaseNotificationsConsumer<EmailNotificationRabbitMqMessage>> logger, IMessageScheduler messageScheduler, IMessageRepository messageRepository, IEmailService emailService) : base(configuration, logger, messageScheduler, messageRepository)
        {
            EmailService = emailService;
        }

        protected override async Task<bool> PerformSpecificBusinessLogic()
        {
            var domainModel = (EmailNotificationMessageDomain)this.ConvertMessageToDomain();
            return await this.EmailService.SendEmail(domainModel);
        }
    }
}
