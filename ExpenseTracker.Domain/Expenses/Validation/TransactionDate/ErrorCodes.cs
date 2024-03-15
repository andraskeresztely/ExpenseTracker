using ExpenseTracker.Domain.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Domain.Expenses.Validation.TransactionDate
{
    [ExcludeFromCodeCoverage]
    public static class ErrorCodes
    {
        public static Error ValueIsInvalid() => new("transaction.date.value.invalid", "Value is invalid.");

        public static Error ValueIsRequired() => new("transaction.date.value.required", "Value is required.");
    }
}
