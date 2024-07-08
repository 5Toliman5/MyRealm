namespace MyRealm.Messaging.Domain.Entities.Domain.Notifications
{
    public class SmsNotificationMessageDomain : BaseDomainMessage
    {
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public SmsNotificationMessageDomain(MessageState state, string message, string phoneNumber) : base(state)
        {
            PhoneNumber = phoneNumber;
            Message = message;
        }
        public SmsNotificationMessageDomain(int id, string message, string phoneNumber)
        {
            this.Id = id;
            this.PhoneNumber = phoneNumber;
            this.Message = message;
        }
    }
}
