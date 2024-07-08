using MyRealm.Messaging.Contracts.Request;
using MyRealm.Messaging.Domain.Entities.Domain;
using MyRealm.Messaging.Domain.Entities.Domain.Notifications;
using MyRealm.Messaging.Domain.Entities.RabbitMq;
using MyRealm.Messaging.Domain.Entities.RabbitMq.Notifications;
using MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Abstract;
using MyRealm.Messaging.Infrastructure.Translators.Request.Abstract;

namespace MyRealm.Messaging.Infrastructure.Translators.Request.Notifications
{
    public class SmsNotificationRequestDomainTranslator : IRequestDomainTranslator<SmsNotificationRequest>
    {
        public BaseDomainMessage ToDomainModel(SmsNotificationRequest request)
        {
            return new SmsNotificationMessageDomain(MessageState.New,request.Message, request.PhoneNumber);
        }
    }
}
