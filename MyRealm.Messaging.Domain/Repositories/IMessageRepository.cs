using MyRealm.Messaging.Domain.Entities.Domain;

namespace MyRealm.Messaging.Domain.Repositories
{
    public interface IMessageRepository
    {
        Task<BaseDomainMessage> Insert(BaseDomainMessage message);
        Task UpdateState(BaseDomainMessage message);
    }
}