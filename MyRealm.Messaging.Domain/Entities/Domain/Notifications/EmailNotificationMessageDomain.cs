namespace MyRealm.Messaging.Domain.Entities.Domain.Notifications
{
    public class EmailNotificationMessageDomain : BaseDomainMessage
    {
        public string EmailAddress { get; set; }
        public string Message { get; set; }
        public EmailNotificationMessageDomain(MessageState state, string message, string emailAddress) : base(state)
        {
            this.State = state;
            this.EmailAddress = emailAddress;
            this.Message = message;
        }
        public EmailNotificationMessageDomain(int id, string message, string emailAddress)
        {
            this.Id = id;
            this.EmailAddress = emailAddress;
            this.Message = message;
        }
    }
}
