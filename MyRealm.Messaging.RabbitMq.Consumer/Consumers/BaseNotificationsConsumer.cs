using MassTransit;
using MyRealm.Messaging.Domain.Entities.Domain;
using MyRealm.Messaging.Domain.Entities.RabbitMq;
using MyRealm.Messaging.Domain.Entities.RabbitMq.Notifications;
using MyRealm.Messaging.Domain.Repositories;
using MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Notifications;
using MyRealm.Messaging.Infrastructure.Translators.Request.Notifications;

namespace MyRealm.Messaging.RabbitMq.Consumer.Consumers
{
    public abstract class BaseNotificationsConsumer<T> : IConsumer<T> where T : BaseNotificationRabbitMqMessage
    {
        protected readonly IConfiguration Configuration;
        protected readonly ILogger<BaseNotificationsConsumer<T>> Logger;
        protected readonly IMessageScheduler MessageScheduler;
        protected readonly IMessageRepository MessageRepository;
        protected T Message { get; set; }
        protected int SendFromHour { get; private set; }
        protected int SendToHour { get; private set; }
        protected int DeliveryDelay { get; private set; }
        protected BaseNotificationsConsumer(IConfiguration configuration, ILogger<BaseNotificationsConsumer<T>> logger, IMessageScheduler messageScheduler, IMessageRepository messageRepository)
        {
            Configuration = configuration;
            Logger = logger;
            this.MessageScheduler = messageScheduler;
            MessageRepository = messageRepository;
        }


        public async Task Consume(ConsumeContext<T> context)
        {
            try
            {
                this.Message = context.Message;
                await this.LoadConfigurationAsync();
                if (this.Message.RetryCount < this.Message.MaxRetriesNumber)
                {
                    this.SetDeliveryDelay();
                    if (this.Message.SendDelayInSeconds == 0)
                    {
                        var messageProvessingResult = await this.PerformSpecificBusinessLogic();
                        if (messageProvessingResult)
                        {
                            this.Logger.LogInformation($"The notification has been sent successfully.");
                            var domainModel = this.ConvertMessageToDomain();
                            domainModel.State = MessageState.Sent;
                            await this.MessageRepository.UpdateState(domainModel);
                            this.Logger.LogInformation($"Consuming finished.");
                            return;
                        }
                        this.Message.IncreaseNumberOfFailedTries();
                        this.Message.SetSendDelayInSeconds(this.DeliveryDelay);

                    }
                    this.Logger.LogInformation($"The notification has not been sent successfully. Initiating a retry.");
                    await this.MessageScheduler.SchedulePublish(TimeSpan.FromSeconds(this.Message.SendDelayInSeconds), this.Message);
                }
                else
                {
                    this.Logger.LogInformation($"Failed sending message: {this.Message.Id}. Updating the status.");
                    var domainModel = this.ConvertMessageToDomain();
                    domainModel.State = MessageState.Failed;
                    await this.MessageRepository.UpdateState(domainModel);
                }

            }
            catch (Exception exception)
            {
                this.Logger.LogError(exception, $"Consuming message {this.Message.Id}");
            }
        }
        protected abstract Task<bool> PerformSpecificBusinessLogic();
        protected BaseDomainMessage ConvertMessageToDomain()
        {
            var domainTranslator = NotificationsRabbitMqDomainTranslatorFactory.GetTranslator(this.Message);
            return domainTranslator.ToDomainModel(this.Message);
        }
        protected virtual void SetDeliveryDelay()
        {
            if (DateTime.UtcNow.Hour > this.SendFromHour && DateTime.UtcNow.Hour < this.SendToHour)
            {
                this.Message.SetSendDelayInSeconds(0);
                return;
            }
            DateTime now = DateTime.Now;

            if (DateTime.UtcNow.Hour < this.SendFromHour)
            {
                var sendTime = now.AddHours(this.SendFromHour);
                var timeDifference = sendTime - now;
                this.Message.SetSendDelayInSeconds((int)timeDifference.TotalSeconds);
                return;
            }

            if (DateTime.UtcNow.Hour >= this.SendToHour)
            {
                var sendTime = now.AddDays(1).AddHours(this.SendFromHour);
                var timeDifference = sendTime - now;
                this.Message.SetSendDelayInSeconds((int)timeDifference.TotalSeconds);
                return;
            }
        }
        private async Task LoadConfigurationAsync()
        {
            this.SendFromHour = this.Configuration.GetValue<int>("Notifications:SendFromHour");
            this.SendToHour = this.Configuration.GetValue<int>("Notifications:SendToHour");
        }
    }
}
