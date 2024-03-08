using Microsoft.Extensions.Options;

namespace BrokerTest
{
    public class KafkaConsumerBackgroundService
    {
        private readonly KafkaSettings _kafkaSettings;
        public KafkaConsumerBackgroundService(IOptionsSnapshot<KafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings.Value;
        }
    }
}
