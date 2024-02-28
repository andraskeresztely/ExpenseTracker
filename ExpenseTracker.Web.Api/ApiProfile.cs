using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;
using ExpenseTracker.Web.Model;

namespace ExpenseTracker.Web.Api
{
    [ExcludeFromCodeCoverage]
    internal sealed class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<Expense, ExpenseViewModel>();

            CreateMap<ExpenseViewModel, Result<Expense>>().ConvertUsing<ExpenseResolver>();
        }

        internal sealed class ExpenseResolver : ITypeConverter<ExpenseViewModel, Result<Expense>>
        {
            public Result<Expense> Convert(ExpenseViewModel source, Result<Expense> destination, ResolutionContext context)
            {
                var result = Expense.Create(
                    source.Id,
                    source.Recipient,
                    source.SpendingAmount!.Value,
                    source.SpendingCurrency,
                    source.TransactionDate!.Value,
                    source.Type);

                return result;
            }
        }
    }
}