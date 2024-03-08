using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace BrokerTest
{
    public class KafkaMessageConsumer
    {
        private readonly KafkaSettings _kafkaSettings;
        public KafkaMessageConsumer(IOptionsSnapshot<KafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings.Value;
        }
        public void Consume()
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _kafkaSettings.BootstrapServers,
                GroupId = "InventoryConsumerGroup",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build())
            {
                consumer.Subscribe(_kafkaSettings.Topic);
                try
                {
                    var result = consumer.Consume();
                    var message = result.Message.Value;
                    Console.WriteLine($"Kafka message received: {message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error");
                }
            }
        }
    }
}
