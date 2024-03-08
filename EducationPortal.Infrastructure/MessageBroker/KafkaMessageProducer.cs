using EducationPortal.Application.Common;
using EducationPortal.Application.Interfaces;
using Microsoft.Extensions.Options;

namespace EducationPortal.Infrastructure.MessageBroker
{
    public class KafkaMessageProducer : IKafkaMessageProducer
    {
        private readonly KafkaSettings _kafkaSettings;
        public KafkaMessageProducer(IOptionsSnapshot<KafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings.Value;
        }
        public async Task PublishMassageAsync<T>(T data)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _kafkaSettings.BootstrapServers
            };
            using var producer = new ProducerBuilder<Null, string>(config).Build();
            var json = JsonConvert.SerializeObject(data);
            var message = new Message<Null, string> { Value = json };
            await producer.ProduceAsync(_kafkaSettings.Topic, message);
        }
    }
}