using AutoMapper;
using CSharpFunctionalExtensions;
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

            CreateMap<ExpenseModel, Result<Expense, Errors>>().ConvertUsing<ExpenseResolver>();
        }

        internal sealed class ExpenseResolver : ITypeConverter<ExpenseModel, Result<Expense, Errors>>
        {
            public Result<Expense, Errors> Convert(
                ExpenseModel source, 
                Result<Expense, Errors> destination, 
                ResolutionContext context)
            {
                var idResult = ExpenseId.Create(source.Id);
                var typeResult = ExpenseType.Create(source.Type);
                var moneyResult = Money.Create(source.SpendingAmount, source.SpendingCurrency);
                var transactionDateResult = TransactionDate.Create(source.TransactionDate);
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
