using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses
{
    public sealed class Expense : Entity<ExpenseId>
    {
        public required Recipient Recipient { get; init; }
        public required Money Spending { get; init; }
        public required TransactionDate TransactionDate { get; init; }
        public required ExpenseType Type { get; init; }

        private Expense() { }

        public static Result<Expense, Errors> Create(
            ExpenseId id,
            Recipient recipient,
            Money spending,
            TransactionDate transactionDate,
            ExpenseType type)
        {
            return new Expense
            {
                Id = id,
                Recipient = recipient,
                Spending = spending,
                TransactionDate = transactionDate,
                Type = type
            };
        }
    }
}
