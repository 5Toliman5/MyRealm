using MyRealm.Messaging.Domain.Entities.RabbitMq;

namespace MyRealm.Messaging.RabbitMQ.Client
{
    public interface IRabbitMqClient
    {
        Task<bool> ScheduleMessage<T>(T message) where T : BaseRabbitMqMessage;
    }
}
