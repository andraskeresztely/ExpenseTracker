using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Persistence.LiteDb.Expenses
{
    public static class Errors
    {
        public static class General
        {
            public static Error NotFound() => new("expense.not.found", "Expense not found.");
        }
    }
}
