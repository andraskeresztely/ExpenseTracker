using Confluent.Kafka;
using System.Text;
using System.Text.Json;

namespace ExpenseTracker.Persistence.Kafka.Serializers
{
    internal sealed class JsonDeserializer<T> : IAsyncDeserializer<T> where T : class
    {
        public Task<T> DeserializeAsync(ReadOnlyMemory<byte> data, bool isNull, SerializationContext context)
        {
            var json = Encoding.UTF8.GetString(data.Span);

            return Task.FromResult(JsonSerializer.Deserialize<T>(json)!);
        }
    }
}
