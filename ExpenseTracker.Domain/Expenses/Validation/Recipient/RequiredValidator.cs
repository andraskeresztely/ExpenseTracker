using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses.Validation.Recipient
{
    internal sealed class RequiredValidator : IValidator<Expenses.Recipient>
    {
        public Maybe<Error> Validate(Expenses.Recipient recipient)
        {
            return string.IsNullOrWhiteSpace(recipient.Name) ? ErrorCodes.NameIsRequired() : Maybe<Error>.None;
        }
    }
}
