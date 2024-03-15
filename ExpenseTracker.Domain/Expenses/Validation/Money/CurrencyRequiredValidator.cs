using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses.Validation.Money
{
    internal sealed class CurrencyRequiredValidator : IValidator<Expenses.Money>
    {
        public Maybe<Error> Validate(Expenses.Money money)
        {
            return string.IsNullOrWhiteSpace(money.Currency) ? ErrorCodes.CurrencyIsRequired() : Maybe<Error>.None;
        }
    }
}
