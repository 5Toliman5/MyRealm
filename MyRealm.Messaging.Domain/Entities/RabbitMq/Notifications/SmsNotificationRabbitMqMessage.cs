namespace MyRealm.Messaging.Domain.Entities.RabbitMq.Notifications
{
    public class SmsNotificationRabbitMqMessage : BaseNotificationRabbitMqMessage
    {
        public string PhoneNumber { get; init; }
        public SmsNotificationRabbitMqMessage(int id, int maxRetriesNumber, string message, string phoneNumber) : base(id, maxRetriesNumber, message)
        {
            PhoneNumber = phoneNumber;
        }

    }
}
