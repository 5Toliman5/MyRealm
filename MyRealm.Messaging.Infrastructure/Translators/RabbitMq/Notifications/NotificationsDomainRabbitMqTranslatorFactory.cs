using MyRealm.Messaging.Contracts.Request;
using MyRealm.Messaging.Domain.Entities.Domain;
using MyRealm.Messaging.Domain.Entities.Domain.Notifications;
using MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Abstract;
using MyRealm.Messaging.Infrastructure.Translators.Request.Abstract;

namespace MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Notifications
{
    public class NotificationsDomainRabbitMqTranslatorFactory
    {
        private static Dictionary<Type, object> translators = new();

        static NotificationsDomainRabbitMqTranslatorFactory()
        {
            translators[typeof(SmsNotificationMessageDomain)] = new SmsNotificationDomainRabbitMqTranslator();
            translators[typeof(EmailNotificationMessageDomain)] = new EmailNotificationDomainRabbitMqTranslator();
        }

        public static IDomainRabbitMqTranslator<T> GetTranslator<T>(T notification) where T : BaseDomainMessage
        {
            var notificationType = notification.GetType();
            if (translators.TryGetValue(notificationType, out object translator))
            {
                return (IDomainRabbitMqTranslator<T>)translator;
            }
            throw new ArgumentException("Invalid notification type");
        }
    }
}
