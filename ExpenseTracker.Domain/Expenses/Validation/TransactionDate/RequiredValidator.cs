using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses.Validation.TransactionDate
{
    internal sealed class RequiredValidator : IValidator<Expenses.TransactionDate>
    {
        public Maybe<Error> Validate(Expenses.TransactionDate transactionDate)
        {
            return transactionDate.Value == DateTime.MinValue ? ErrorCodes.ValueIsRequired() : Maybe<Error>.None;
        }
    }
}
