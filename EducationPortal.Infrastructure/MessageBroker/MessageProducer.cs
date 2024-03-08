using EducationPortal.Application.Common;
using EducationPortal.Application.Interfaces;
using Microsoft.Extensions.Options;

namespace EducationPortal.Infrastructure.MessageBroker
{
    public class MessageProducer : IMessageProducer
    {
        private readonly BrokerSettings _brokerSettings;
        public MessageProducer(IOptionsSnapshot<BrokerSettings> brokerSettings)
        {
            _brokerSettings = brokerSettings.Value;
        }
        public void PublishMassage<T>(T message)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(_brokerSettings.Uri);
            factory.ClientProvidedName = _brokerSettings.ClientName;

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(_brokerSettings.ExchangeName, ExchangeType.Direct);
            channel.QueueDeclare(_brokerSettings.QueueName, false, false, false, null);
            channel.QueueBind(_brokerSettings.QueueName, _brokerSettings.ExchangeName, _brokerSettings.RoutingKey, null);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(_brokerSettings.ExchangeName, _brokerSettings.RoutingKey, null, body);

            channel.Close();
            connection.Close();
        }
    }
}
