using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BrokerTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();

            IServiceCollection services = new ServiceCollection();
            services.Configure<BrokerSettings>(configuration.GetSection("BrokerSettings"));
            services.Configure<KafkaSettings>(configuration.GetSection("KafkaSettings"));
            services.AddScoped<MessageConsumer>();
            services.AddScoped<KafkaMessageConsumer>();

            var serviceProvider = services.BuildServiceProvider();
            var consumer = serviceProvider.GetRequiredService<MessageConsumer>();
            var kafkaConsumer = serviceProvider.GetRequiredService<KafkaMessageConsumer>();
            consumer.Consume();
            kafkaConsumer.Consume();
        }
    }
}
