using MyRealm.Messaging.Domain.Entities.Domain;
using MyRealm.Messaging.Domain.Entities.RabbitMq;
namespace MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Abstract
{
    public interface IRabbitMqDomainTranslator<in T> where T : BaseRabbitMqMessage
    {
        BaseDomainMessage ToDomainModel(T rabbitMqMessage);
    }
}
