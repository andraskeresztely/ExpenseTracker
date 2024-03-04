using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses.Persistence
{
    public interface IExpenseRepository
    {
        Task<int> CreateAsync(Expense expense);

        Task DeleteAsync(int id);

        IAsyncEnumerable<Result<Expense, Errors>> GetAllAsync();

        Task<Result<Expense, Errors>> GetAsync(int id);

        Task<bool> UpdateAsync(Expense expense);
    }
}
