using MyRealm.Messaging.Contracts.Request;
using MyRealm.Messaging.Domain.Entities.Domain;
using MyRealm.Messaging.Domain.Entities.Domain.Notifications;
using MyRealm.Messaging.Domain.Entities.RabbitMq;
using MyRealm.Messaging.Domain.Entities.RabbitMq.Notifications;
using MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Abstract;
using MyRealm.Messaging.Infrastructure.Translators.Request.Abstract;

namespace MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Notifications
{
    public class EmailNotificationDomainRabbitMqTranslator : IDomainRabbitMqTranslator<EmailNotificationMessageDomain>
    {
        public BaseRabbitMqMessage ToRabbitMqMessage(EmailNotificationMessageDomain domain, MessageConfiguration configuration)
        {
            return new EmailNotificationRabbitMqMessage(domain.Id, configuration.MaxRetriesNumber, domain.Message, domain.EmailAddress);
        }
    }
}
