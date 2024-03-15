using ExpenseTracker.Domain.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Domain.Expenses.Validation.Money
{
    [ExcludeFromCodeCoverage]
    public static class ErrorCodes
    {
        public static Error AmountIsInvalid() => new("spending.amount.invalid", "Spending amount is invalid.");

        public static Error CurrencyIsInvalid() => new("spending.currency.invalid", "Spending currency is invalid.");

        public static Error CurrencyIsRequired() => new("spending.currency.required", "Spending currency is required.");
    }
}
