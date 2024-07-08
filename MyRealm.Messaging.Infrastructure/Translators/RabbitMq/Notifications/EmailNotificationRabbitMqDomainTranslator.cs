using MyRealm.Messaging.Domain.Entities.Domain;
using MyRealm.Messaging.Domain.Entities.Domain.Notifications;
using MyRealm.Messaging.Domain.Entities.RabbitMq.Notifications;
using MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Abstract;

namespace MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Notifications
{
    internal class EmailNotificationRabbitMqDomainTranslator : IRabbitMqDomainTranslator<EmailNotificationRabbitMqMessage>
    {
        public BaseDomainMessage ToDomainModel(EmailNotificationRabbitMqMessage rabbitMqMessage)
        {
            return new EmailNotificationMessageDomain(rabbitMqMessage.Id, rabbitMqMessage.Message, rabbitMqMessage.EmailAddress);
        }
    }
}
