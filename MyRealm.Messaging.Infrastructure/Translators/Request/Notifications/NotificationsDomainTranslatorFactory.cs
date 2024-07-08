using MyRealm.Messaging.Contracts.Request;
using MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Abstract;
using MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Notifications;
using MyRealm.Messaging.Infrastructure.Translators.Request.Abstract;

namespace MyRealm.Messaging.Infrastructure.Translators.Request.Notifications
{
    public class NotificationsDomainTranslatorFactory
    {
        private static Dictionary<Type, object> translators = new();

        static NotificationsDomainTranslatorFactory()
        {
            translators[typeof(SmsNotificationRequest)] = new SmsNotificationRequestDomainTranslator();
            translators[typeof(EmailNotificationRequest)] = new EmailNotificationRequestDomainTranslator();
        }

        public static IRequestDomainTranslator<T> GetTranslator<T>(T notification)
        {
            var notificationType = notification.GetType();
            if (translators.TryGetValue(notificationType, out object translator))
            {
                return (IRequestDomainTranslator<T>)translator;
            }
            throw new ArgumentException("Invalid notification type");
        }
    }
}
