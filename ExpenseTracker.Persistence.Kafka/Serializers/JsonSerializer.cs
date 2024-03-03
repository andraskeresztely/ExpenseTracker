using Confluent.Kafka;
using System.Text;
using System.Text.Json;

namespace ExpenseTracker.Persistence.Kafka.Serializers
{
    internal sealed class JsonSerializer<T> : IAsyncSerializer<T> where T : class
    {
        Task<byte[]> IAsyncSerializer<T>.SerializeAsync(T data, SerializationContext context)
        {
            var jsonString = JsonSerializer.Serialize(data);

            return Task.FromResult(Encoding.UTF8.GetBytes(jsonString));
        }
    }
}
