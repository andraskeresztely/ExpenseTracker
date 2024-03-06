using Confluent.Kafka;
using ExpenseTracker.Persistence.Kafka.Expenses;
using ExpenseTracker.Persistence.Kafka.Serializers;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Persistence.Kafka
{
    [ExcludeFromCodeCoverage]
    internal sealed class KafkaClient(
        IOptions<ProducerConfig> producerConfig,
        IOptions<ConsumerConfig> consumerConfig) : IDisposable
    {
        public IConsumer<string, ExpenseModel> Consumer { get; } 
            = new ConsumerBuilder<string, ExpenseModel>(consumerConfig.Value)
                .SetKeyDeserializer(Deserializers.Utf8)
                .SetValueDeserializer(new JsonDeserializer<ExpenseModel>())
                .Build();

        public IProducer<string, ExpenseModel> Producer { get; } 
            = new ProducerBuilder<string, ExpenseModel>(producerConfig.Value)
                .SetValueSerializer(new JsonSerializer<ExpenseModel>())
                .Build();

        public void Dispose()
        {
            Producer.Flush();
            Producer.Dispose();

            Consumer.Close();
            Consumer.Dispose();
        }
    }
}
