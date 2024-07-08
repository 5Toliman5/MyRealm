namespace MyRealm.Messaging.Domain.Entities.Domain
{
    public abstract class BaseDomainMessage
    {
        public int Id {  get; set; }
        public MessageState State { get;  set; }
        protected BaseDomainMessage()
        {
        }
        protected BaseDomainMessage(MessageState state)
        {
            this.State = state;
        }
    }
}
