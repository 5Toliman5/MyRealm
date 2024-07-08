using MyRealm.Messaging.Domain.Entities.Domain;
using MyRealm.Messaging.Domain.Entities.Domain.Notifications;
using MyRealm.Messaging.Domain.Entities.RabbitMq.Notifications;
using MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Notifications
{
    internal class SmsNotificationRabbitMqDomainTranslator : IRabbitMqDomainTranslator<SmsNotificationRabbitMqMessage>
    {
        public BaseDomainMessage ToDomainModel(SmsNotificationRabbitMqMessage rabbitMqMessage)
        {
            return new SmsNotificationMessageDomain(rabbitMqMessage.Id, rabbitMqMessage.Message, rabbitMqMessage.PhoneNumber);
        }
    }
}
