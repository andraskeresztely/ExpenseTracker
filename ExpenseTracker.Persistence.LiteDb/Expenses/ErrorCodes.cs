using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Persistence.LiteDb.Expenses
{
    internal static class ErrorCodes
    {
        public static class General
        {
            public static Error NotFound() => new("expense.not.found", "Expense not found.");
        }
    }
}
