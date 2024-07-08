using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRealm.Messaging.Contracts;
using MyRealm.Messaging.Contracts.Request;
using MyRealm.Messaging.Domain.Entities.Domain;
using MyRealm.Messaging.Domain.Repositories;
using MyRealm.Messaging.Infrastructure;
using MyRealm.Messaging.Infrastructure.Translators.RabbitMq.Notifications;
using MyRealm.Messaging.Infrastructure.Translators.Request.Notifications;
using MyRealm.Messaging.RabbitMQ.Client;
using System.Xml.Serialization;

namespace MyRealm.Messaging.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly ILogger<NotificationsController> Logger;
        private readonly IMessageRepository Repository;
        private readonly MessageConfiguration MessageConfiguration;
        private readonly IRabbitMqClient RabbitMqClient;

        public NotificationsController(ILogger<NotificationsController> logger, IMessageRepository repository, MessageConfiguration messageConfiguration, IRabbitMqClient rabbitMqClient)
        {
            this.Logger = logger;
            this.Repository = repository;
            this.MessageConfiguration = messageConfiguration;
            this.RabbitMqClient = rabbitMqClient;
        }
        [HttpPost]
        [Consumes("application/xml")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<IActionResult> HandleNotification()
        {
            this.Logger.LogInformation("HandleNotification started.");
            var request = (dynamic)await DeserializeRequestAsync();
            if (request == null)
            {
                return BadRequest("Invalid message.");
            }
            request.Validate();
            var domainTranslator = NotificationsDomainTranslatorFactory.GetTranslator(request);
            var domainModel = (BaseDomainMessage)domainTranslator.ToDomainModel(request);
            domainModel = await this.Repository.Insert(domainModel);
            var rabbitMqTranslator = NotificationsDomainRabbitMqTranslatorFactory.GetTranslator((dynamic)domainModel);
            var rabbitMqRequest = rabbitMqTranslator.ToRabbitMqMessage((dynamic)domainModel, this.MessageConfiguration);
            await this.RabbitMqClient.ScheduleMessage(rabbitMqRequest);
            return this.Ok();
        }
        private async Task<object> DeserializeRequestAsync()
        {
            using var requestStream = new StreamReader(this.Request.Body);
            var requestString = await requestStream.ReadToEndAsync();
            var requestType = GetRequestType(requestString);
            var deserializer = new XmlSerializer(requestType);
            return deserializer.Deserialize(new StringReader(requestString));
        }
        private Type GetRequestType(string request)
        {
            if (request.Contains(Constants.EmailNotificationRequest))
                return typeof(EmailNotificationRequest);
            if (request.Contains(Constants.SmsNotificationRequest))
                return typeof(SmsNotificationRequest);
            throw new KeyNotFoundException("Can't find mapping for the request.");
        }
    }
}
