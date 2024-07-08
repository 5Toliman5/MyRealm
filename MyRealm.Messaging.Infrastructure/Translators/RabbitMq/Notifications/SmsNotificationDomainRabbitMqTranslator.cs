using MyRealm.Messaging.Contracts.Request;
using MyRealm.Messaging.Domain.Entities.Domain;
using MyRealm.Messaging.Domain.Entities.Domain.Notifications;
using MyRealm.Messaging.Domain.Entities.RabbitMq;
using MyRealm.Messaging.Domain.Entities.RabbitMq.Notifications;
using MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Abstract;
using MyRealm.Messaging.Infrastructure.Translators.Request.Abstract;

namespace MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Notifications
{
    public class SmsNotificationDomainRabbitMqTranslator : IDomainRabbitMqTranslator<SmsNotificationMessageDomain>
    {
        public BaseRabbitMqMessage ToRabbitMqMessage(SmsNotificationMessageDomain request, MessageConfiguration configuration)
        {
            return new SmsNotificationRabbitMqMessage(request.Id, configuration.MaxRetriesNumber, request.Message, request.PhoneNumber);
        }
    }
}
