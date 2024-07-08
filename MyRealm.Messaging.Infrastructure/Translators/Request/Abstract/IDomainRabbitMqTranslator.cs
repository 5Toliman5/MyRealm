using MyRealm.Messaging.Domain.Entities.Domain;
using MyRealm.Messaging.Domain.Entities.RabbitMq;

namespace MyRealm.Messaging.Infrastructure.Translators.Request.Abstract
{
    public interface IRequestDomainTranslator<in T>
    {
        BaseDomainMessage ToDomainModel(T request);
    }
}
