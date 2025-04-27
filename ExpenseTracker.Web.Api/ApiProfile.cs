using AutoMapper;
using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;
using ExpenseTracker.Web.Model;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Web.Api
{
    [ExcludeFromCodeCoverage]
    internal sealed class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<Expense, ExpenseViewModel>();

            CreateMap<ExpenseViewModel, Result<Expense, Errors>>().ConvertUsing<ExpenseResolver>();
        }

        internal sealed class ExpenseResolver : ITypeConverter<ExpenseViewModel, Result<Expense, Errors>>
        {
            public Result<Expense, Errors> Convert(
                ExpenseViewModel source,
                Result<Expense, Errors> destination,
                ResolutionContext context)
            {
                var idResult = ExpenseId.Create(source.Id);
                var typeResult = ExpenseType.Create(source.Type);
                var moneyResult = Money.Create(source.SpendingAmount!.Value, source.SpendingCurrency);
                var transactionDateResult = TransactionDate.Create(source.TransactionDate!.Value);
                var recipientResult = Recipient.Create(source.Recipient);

                var partsResult = Result.Combine<object, Errors>(idResult, typeResult, moneyResult, transactionDateResult, recipientResult);
                if (partsResult.IsFailure)
                {
                    return partsResult.Error;
                }

                var expenseResult = Expense.Create(
                    idResult.Value,
                    recipientResult.Value,
                    moneyResult.Value,
                    transactionDateResult.Value,
                    typeResult.Value);

                return expenseResult;
            }
        }
    }
}