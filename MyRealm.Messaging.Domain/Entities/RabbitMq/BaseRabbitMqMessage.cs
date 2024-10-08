namespace MyRealm.Messaging.Domain.Entities.RabbitMq
{
	public abstract class BaseRabbitMqMessage
    {
        public int Id { get; set; }
        public int MaxRetriesNumber { get; init; }
        public int RetryCount { get; private set; }
        public int SendDelayInSeconds { get; private set; }

        protected BaseRabbitMqMessage(int id, int maxRetriesNumber)
        {
			this.Id = id;
			this.MaxRetriesNumber = maxRetriesNumber;
        }

        public void IncreaseNumberOfFailedTries()
        {
			this.RetryCount++;
        }
        public void SetSendDelayInSeconds(int delayInSeconds)
        {
            this.SendDelayInSeconds = delayInSeconds;
        }
    }
}
