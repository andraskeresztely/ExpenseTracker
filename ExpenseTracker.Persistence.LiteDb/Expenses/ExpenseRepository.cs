using AutoMapper;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;
using ExpenseTracker.Domain.Expenses.Persistence;
using LiteDB;

namespace ExpenseTracker.Persistence.LiteDb.Expenses
{
    public sealed class ExpenseRepository(
        ILiteDatabase dbContext,
        IMapper mapper) : IExpenseRepository
    {
        public async Task DeleteAsync(int id)
        {
            var expenses = dbContext.GetCollection<ExpenseModel>();

            expenses.Delete(id);

            await Task.CompletedTask;
        }

        public async Task<Result<Expense>> GetAsync(int id)
        {
            var expenses = dbContext.GetCollection<ExpenseModel>();

            var expenseModel = expenses.FindById(id);

            if (expenseModel == null)
            {
                return await Task.FromResult(new ErrorList([ Errors.General.NotFound() ]));
            }

            var expense = Expense.Create(
                expenseModel.Id,
                expenseModel.Recipient,
                expenseModel.SpendingAmount,
                expenseModel.SpendingCurrency,
                expenseModel.TransactionDate,
                expenseModel.Type);

            return await Task.FromResult(expense);
        }

        public async IAsyncEnumerable<Result<Expense>> GetAllAsync()
        {
            var expenses = dbContext.GetCollection<ExpenseModel>();

            foreach (var expenseModel in expenses.Query().ToEnumerable())
            {
                var expense = Expense.Create(
                    expenseModel.Id,
                    expenseModel.Recipient,
                    expenseModel.SpendingAmount,
                    expenseModel.SpendingCurrency,
                    expenseModel.TransactionDate,
                    expenseModel.Type);

                yield return await Task.FromResult(expense);
            }
        }

        public async Task<int> InsertAsync(Expense expense)
        {
            var expenses = dbContext.GetCollection<ExpenseModel>();

            var expenseModel = mapper.Map<ExpenseModel>(expense);

            expenses.Insert(expenseModel);

            return await Task.FromResult(expenseModel.Id);
        }

        public async Task<bool> UpdateAsync(Expense expense)
        {
            var expenses = dbContext.GetCollection<ExpenseModel>();

            var expenseModel = mapper.Map<ExpenseModel>(expense);

            var result = expenses.Update(expenseModel);

            return await Task.FromResult(result);
        }
    }
}
