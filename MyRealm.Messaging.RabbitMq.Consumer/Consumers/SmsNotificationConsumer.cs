using MassTransit;
using MyRealm.Messaging.Domain.Entities.Domain.Notifications;
using MyRealm.Messaging.Domain.Entities.RabbitMq.Notifications;
using MyRealm.Messaging.Domain.Repositories;
using MyRealm.Messaging.Domain.Services.Notifications;

namespace MyRealm.Messaging.RabbitMq.Consumer.Consumers
{
    public class SmsNotificationConsumer : BaseNotificationsConsumer<SmsNotificationRabbitMqMessage>
    {
        private readonly ISmsService SmsService;

        public SmsNotificationConsumer(IConfiguration configuration, ILogger<BaseNotificationsConsumer<SmsNotificationRabbitMqMessage>> logger, IMessageScheduler messageScheduler, IMessageRepository messageRepository, ISmsService smsService) : base(configuration, logger, messageScheduler, messageRepository)
        {
            SmsService = smsService;
        }

        protected override async Task<bool> PerformSpecificBusinessLogic()
        {
            var domainModel = (SmsNotificationMessageDomain)this.ConvertMessageToDomain();
            return await this.SmsService.SendSms(domainModel);
        }
    }
}
