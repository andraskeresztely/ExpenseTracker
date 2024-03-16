using ExpenseTracker.Domain.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Domain.Expenses.Validation.Recipient
{
    [ExcludeFromCodeCoverage]
    internal static class ErrorCodes
    {
        public static Error LengthIsInvalid() => new("recipient.length.invalid", "Length is invalid.");

        public static Error NameIsRequired() => new("recipient.name.required", "Name is required.");
    }
}
