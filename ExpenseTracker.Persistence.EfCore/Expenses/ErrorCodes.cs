using ExpenseTracker.Domain.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Persistence.EfCore.Expenses
{
    [ExcludeFromCodeCoverage]
    internal static class ErrorCodes
    {
        public static class General
        {
            public static Error NotFound() => new("expense.not.found", "Expense not found.");
        }
    }
}
