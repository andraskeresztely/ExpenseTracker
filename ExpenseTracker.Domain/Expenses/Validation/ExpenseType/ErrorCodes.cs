using ExpenseTracker.Domain.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Domain.Expenses.Validation.ExpenseType
{
    [ExcludeFromCodeCoverage]
    internal static class ErrorCodes
    {
        public static Error ValueIsInvalid() => new("expense.type.value.invalid", "Value is invalid.");

        public static Error ValueIsRequired() => new("expense.type.value.required", "Value is required.");
    }
}
