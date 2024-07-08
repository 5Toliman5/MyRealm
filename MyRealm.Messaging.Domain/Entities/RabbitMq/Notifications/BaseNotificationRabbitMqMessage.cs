namespace MyRealm.Messaging.Domain.Entities.RabbitMq.Notifications
{
    public abstract class BaseNotificationRabbitMqMessage : BaseRabbitMqMessage
    {
        public string Message { get; init; }
        protected BaseNotificationRabbitMqMessage(int id, int maxRetriesNumber, string message) : base(id, maxRetriesNumber)
        {
            Message = message;
        }
    }
}
