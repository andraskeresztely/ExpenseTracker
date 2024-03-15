using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses.Validation.Recipient
{
    internal sealed class ValueValidator : IValidator<Expenses.Recipient>
    {
        public Maybe<Error> Validate(Expenses.Recipient recipient)
        {
            return recipient.Name.Length is < Rules.Recipient.MinRecipientLength or > Rules.Recipient.MaxRecipientLength
                ? ErrorCodes.LengthIsInvalid()
                : Maybe<Error>.None;
        }
    }
}
