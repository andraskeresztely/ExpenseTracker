using AutoMapper;
using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;
using ExpenseTracker.Domain.Expenses.Persistence;
using LiteDB;

namespace ExpenseTracker.Persistence.LiteDb.Expenses
{
    internal sealed class ExpenseRepository(
        ILiteDatabase dbContext,
        IMapper mapper) : IExpenseRepository
    {
        public Task<int> CreateAsync(Expense expense)
        {
            var expenses = dbContext.GetCollection<ExpenseModel>();

            var expenseModel = mapper.Map<ExpenseModel>(expense);

            expenses.Insert(expenseModel);

            return Task.FromResult(expenseModel.Id);
        }

        public Task DeleteAsync(int id)
        {
            var expenses = dbContext.GetCollection<ExpenseModel>();

            expenses.Delete(id);

            return Task.CompletedTask;
        }

        public async IAsyncEnumerable<Result<Expense, Errors>> GetAllAsync()
        {
            var expenses = dbContext.GetCollection<ExpenseModel>();

            foreach (var expenseModel in expenses.Query().ToEnumerable())
            {
                var result = mapper.Map<Result<Expense, Errors>>(expenseModel);

                yield return await Task.FromResult(result);
            }
        }

        public Task<Result<Expense, Errors>> GetAsync(int id)
        {
            var expenses = dbContext.GetCollection<ExpenseModel>();

            var expenseModel = expenses.FindById(id);

            if (expenseModel == null)
            {
                Result<Expense, Errors> errorResult = new Errors([ErrorCodes.General.NotFound()]);

                return Task.FromResult(errorResult);
            }

            var result = mapper.Map<Result<Expense, Errors>>(expenseModel);

            return Task.FromResult(result);
        }

        public Task<bool> UpdateAsync(Expense expense)
        {
            var expenses = dbContext.GetCollection<ExpenseModel>();

            var expenseModel = mapper.Map<ExpenseModel>(expense);

            var result = expenses.Update(expenseModel);

            return Task.FromResult(result);
        }
    }
}
