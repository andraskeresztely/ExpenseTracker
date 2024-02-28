using AutoMapper;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;
using ExpenseTracker.Persistence.LiteDb.Expenses;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Persistence.LiteDb
{
    [ExcludeFromCodeCoverage]
    public sealed class LiteDbProfile : Profile
    {
        public LiteDbProfile()
        {
            CreateMap<Expense, ExpenseModel>();

            CreateMap<ExpenseModel, Result<Expense>>().ConvertUsing<ExpenseResolver>();
        }

        internal sealed class ExpenseResolver : ITypeConverter<ExpenseModel, Result<Expense>>
        {
            public Result<Expense> Convert(ExpenseModel source, Result<Expense> destination, ResolutionContext context)
            {
                var result = Expense.Create(
                    source.Id,
                    source.Recipient,
                    source.SpendingAmount,
                    source.SpendingCurrency,
                    source.TransactionDate,
                    source.Type);

                return result;
            }
        }
    }
}
