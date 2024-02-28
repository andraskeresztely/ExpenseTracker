using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses.Persistence
{
    public interface IExpenseRepository
    {
        Task DeleteAsync(int id);

        IAsyncEnumerable<Result<Expense>> GetAllAsync();

        Task<Result<Expense>> GetAsync(int id);

        Task<int> InsertAsync(Expense expense);

        Task<bool> UpdateAsync(Expense expense);
    }
}
