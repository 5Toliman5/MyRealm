namespace MyRealm.Messaging.Domain.Entities.RabbitMq.Notifications
{
    public class EmailNotificationRabbitMqMessage : BaseNotificationRabbitMqMessage
    {

        public string EmailAddress { get; init; }
        public EmailNotificationRabbitMqMessage(int id, int maxRetriesNumber, string message, string emailAddress) : base(id, maxRetriesNumber, message)
        {
            EmailAddress = emailAddress;
        }


    }
}
