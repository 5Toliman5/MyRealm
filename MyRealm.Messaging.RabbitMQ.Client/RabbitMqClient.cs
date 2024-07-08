using MassTransit;
using Microsoft.Extensions.Logging;
using MyRealm.Messaging.Domain.Entities.RabbitMq;

namespace MyRealm.Messaging.RabbitMQ.Client
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private readonly ILogger<RabbitMqClient> Logger;
        private readonly IMessageScheduler MessageScheduler;
        private readonly int SendDelayInSeconds;
        public RabbitMqClient(ILogger<RabbitMqClient> logger, IMessageScheduler messageScheduler, int sendDelayInSeconds)
        {
            this.Logger = logger;
            this.SendDelayInSeconds = sendDelayInSeconds;
            this.MessageScheduler = messageScheduler;
        }

        public async Task<bool> ScheduleMessage<T>(T message) where T : BaseRabbitMqMessage
        {
            this.Logger.LogInformation($"Queuing {message.GetType()} notification started.");
            var result = await this.MessageScheduler.SchedulePublish(TimeSpan.FromSeconds(this.SendDelayInSeconds), message);
            this.Logger.LogInformation($"Queuing {message.GetType()} notification ended.");
            return !string.IsNullOrEmpty(result.TokenId.ToString());
        }
    }
}
