using Confluent.Kafka;
using System.Text;
using System.Text.Json;

namespace ExpenseTracker.Persistence.Kafka.Serializers
{
    internal sealed class JsonDeserializer<T> : IDeserializer<T> where T : class
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            var json = Encoding.UTF8.GetString(data);

            return JsonSerializer.Deserialize<T>(json)!;
        }
    }
}
