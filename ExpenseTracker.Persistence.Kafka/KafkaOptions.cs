using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Persistence.Kafka
{
    [ExcludeFromCodeCoverage]
    public sealed class KafkaOptions
    {
        public int TimeoutSeconds { get; set; } = 1;

        public string Topic { get; set; } = string.Empty;
    }
}
