using MassTransit;
using Microsoft.Extensions.Logging;
using MyRealm.Messaging.Domain.Entities.RabbitMq;

namespace MyRealm.Messaging.RabbitMq.Client
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private readonly ILogger<RabbitMqClient> Logger;
        private readonly IMessageScheduler MessageScheduler;
        private readonly int SendDelayInSeconds;
        public RabbitMqClient(ILogger<RabbitMqClient> logger, IMessageScheduler messageScheduler, int sendDelayInSeconds)
        {
            Logger = logger;
            SendDelayInSeconds = sendDelayInSeconds;
            MessageScheduler = messageScheduler;
        }

        public async Task<bool> ScheduleMessage<T>(T message) where T : BaseRabbitMqMessage
        {
            Logger.LogInformation($"Queuing {message.GetType()} notification started.");
            var result = await MessageScheduler.SchedulePublish(TimeSpan.FromSeconds(SendDelayInSeconds), message);
            Logger.LogInformation($"Queuing {message.GetType()} notification ended.");
            return !string.IsNullOrEmpty(result.TokenId.ToString());
        }
    }
}
