using RabbitMQ.Client;
using System.Text;

namespace MyRealm.Messaging.RabbitMQ.Client
{
    public class RabbitMqQueueCreator
    {
        private readonly string _hostAddress;

        public RabbitMqQueueCreator(string hostAddress)
        {
            this._hostAddress = hostAddress;
        }
        public void Create(string name = "default", bool durable = true, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> optionalArguments = null)
        {
            var factory = new ConnectionFactory() { HostName = _hostAddress };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: name,
                     durable: durable,
                     exclusive: exclusive,
                     autoDelete: autoDelete,
                     arguments: optionalArguments);
        }
    }
}
