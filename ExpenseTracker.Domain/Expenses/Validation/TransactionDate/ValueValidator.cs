using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses.Validation.TransactionDate
{
    internal sealed class ValueValidator : IValidator<Expenses.TransactionDate>
    {
        public Maybe<Error> Validate(Expenses.TransactionDate transactionDate)
        {
            return transactionDate.Value > Rules.TransactionDate.MaxTransactionDate.Date
                ? ErrorCodes.ValueIsInvalid()
                : Maybe<Error>.None;
        }
    }
}
