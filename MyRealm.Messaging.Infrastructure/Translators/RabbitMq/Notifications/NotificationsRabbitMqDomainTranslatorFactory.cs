using MyRealm.Messaging.Domain.Entities.Domain.Notifications;
using MyRealm.Messaging.Domain.Entities.RabbitMq;
using MyRealm.Messaging.Domain.Entities.RabbitMq.Notifications;
using MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Abstract;
using MyRealm.Messaging.Infrastructure.Translators.Request.Abstract;

namespace MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Notifications
{
    public class NotificationsRabbitMqDomainTranslatorFactory
    {
        private static Dictionary<Type, object> translators = new();

        static NotificationsRabbitMqDomainTranslatorFactory()
        {
            translators[typeof(SmsNotificationRabbitMqMessage)] = new SmsNotificationRabbitMqDomainTranslator();
            translators[typeof(EmailNotificationRabbitMqMessage)] = new EmailNotificationRabbitMqDomainTranslator();
        }

        public static IRabbitMqDomainTranslator<T> GetTranslator<T>(T notification) where T : BaseRabbitMqMessage 
        {
            var notificationType = notification.GetType();
            if (translators.TryGetValue(notificationType, out object translator))
            {
                return (IRabbitMqDomainTranslator<T>)translator;
            }
            throw new ArgumentException("Invalid notification type");
        }
    }
}
