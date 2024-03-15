using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses.Validation.Money
{
    internal sealed class CurrencyValueValidator : IValidator<Expenses.Money>
    {
        public Maybe<Error> Validate(Expenses.Money money)
        {
            return Rules.Spending.AllCurrencies.Any(currency => string.Equals(currency, money.Currency, StringComparison.Ordinal))
                ? Maybe<Error>.None
                : ErrorCodes.CurrencyIsInvalid();
        }
    }
}
