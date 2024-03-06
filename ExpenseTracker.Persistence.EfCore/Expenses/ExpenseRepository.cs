using AutoMapper;
using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;
using ExpenseTracker.Domain.Expenses.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Persistence.EfCore.Expenses
{
    internal sealed class ExpenseRepository(IMapper mapper, DbContextOptions<ExpenseContext> options) : IExpenseRepository 
    {
        public async Task<int> CreateAsync(Expense expense)
        {
            await using var dbContext = new ExpenseContext(options);

            var expenseModel = mapper.Map<ExpenseModel>(expense);

            dbContext.Expenses.Add(expenseModel);

            return await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await using var dbContext = new ExpenseContext(options);

            var expenseModel = dbContext.Expenses.FirstOrDefault(expense => expense.Id == id);

            if (expenseModel != null)
            {
                dbContext.Expenses.Remove(expenseModel);

                await dbContext.SaveChangesAsync();
            }
        }

        public async IAsyncEnumerable<Result<Expense, Errors>> GetAllAsync()
        {
            await using var dbContext = new ExpenseContext(options);

            foreach (var expenseModel in dbContext.Expenses)
            {
                var result = mapper.Map<Result<Expense, Errors>>(expenseModel);

                yield return result;
            }
        }

        public async Task<Result<Expense, Errors>> GetAsync(int id)
        {
            await using var dbContext = new ExpenseContext(options);

            var expenseModel = dbContext.Expenses.FirstOrDefault(expense => expense.Id == id);

            if (expenseModel == null)
            {
                Result<Expense, Errors> errorResult = new Errors([ErrorCodes.General.NotFound()]);

                return errorResult;
            }

            var result = mapper.Map<Result<Expense, Errors>>(expenseModel);

            return result;
        }

        public async Task<bool> UpdateAsync(Expense expense)
        {
            await using var dbContext = new ExpenseContext(options);

            var expenseModel = mapper.Map<ExpenseModel>(expense);

            dbContext.Expenses.Update(expenseModel);

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
