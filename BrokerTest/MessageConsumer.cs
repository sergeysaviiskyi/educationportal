using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace BrokerTest
{
    public class MessageConsumer
    {
        private readonly BrokerSettings _brokerSettings;
        public MessageConsumer(IOptionsSnapshot<BrokerSettings> brokerOptions)
        {
            _brokerSettings = brokerOptions.Value;
        }
        public void Consume()
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(_brokerSettings.Uri);
            factory.ClientProvidedName = _brokerSettings.ClientName;

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(_brokerSettings.ExchangeName, ExchangeType.Direct);
            channel.QueueDeclare(_brokerSettings.QueueName, false, false, false, null);
            channel.QueueBind(_brokerSettings.QueueName, _brokerSettings.ExchangeName, _brokerSettings.RoutingKey, null);
            channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, args) =>
            {
                var body = args.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"RabbitMQ message received: {message}");
                channel.BasicAck(args.DeliveryTag, false);
            };
            channel.BasicConsume(_brokerSettings.QueueName, false, consumer);
            Console.ReadLine();
        }
    }
}
