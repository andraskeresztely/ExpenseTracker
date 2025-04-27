using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Persistence.Kafka.Expenses
{
    [ExcludeFromCodeCoverage]
    internal sealed class ExpenseModel
    {
        public required int Id { get; init; }

        public required string Recipient { get; init; }

        public required decimal SpendingAmount { get; init; }

        public required string SpendingCurrency { get; init; }

        public required DateTime TransactionDate { get; init; }

        public required string Type { get; init; }
    }
}
