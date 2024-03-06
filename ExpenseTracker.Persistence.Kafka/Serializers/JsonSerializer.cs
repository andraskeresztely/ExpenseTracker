using Confluent.Kafka;
using System.Text;
using System.Text.Json;

namespace ExpenseTracker.Persistence.Kafka.Serializers
{
    internal sealed class JsonSerializer<T> : ISerializer<T> where T : class
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            var jsonString = JsonSerializer.Serialize(data);

            return Encoding.UTF8.GetBytes(jsonString);
        }
    }
}
