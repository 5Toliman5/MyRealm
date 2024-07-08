using MyRealm.Messaging.Domain.Entities.Domain;
using MyRealm.Messaging.Domain.Entities.RabbitMq;

namespace MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Abstract
{
    public interface IDomainRabbitMqTranslator<in T> where T : BaseDomainMessage
    {
        BaseRabbitMqMessage ToRabbitMqMessage(T domain, MessageConfiguration configuration);
    }
}
