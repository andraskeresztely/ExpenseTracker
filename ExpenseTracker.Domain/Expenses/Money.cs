using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses.Validation;

namespace ExpenseTracker.Domain.Expenses
{
    public sealed class Money : ValueObject
    {
        public required decimal Amount { get; init; }
        public required string Currency { get; init; }

        private Money() {}

        public static Result<Money, Errors> Create(decimal amount, string currency)
        {
            var (isValid, errors) = IsValid(amount, currency);

            if (!isValid)
            {
                return new Errors(errors);
            }

            return new Money { Amount = amount, Currency = currency };
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        public override string ToString()
        {
            return $"{Currency} {Amount}";
        }

        private static (bool IsValid, List<Error> Errors) IsValid(decimal amount, string currency)
        {
            List<Error> errors = [];

            if (amount is < Rules.Spending.MinAmount or > Rules.Spending.MaxAmount)
            {
                errors.Add(ErrorCodes.Spending.AmountIsInvalid());
            }

            if (string.IsNullOrWhiteSpace(currency))
            {
                errors.Add(ErrorCodes.Spending.CurrencyIsRequired());
            }
            else if (!Rules.Spending.AllCurrencies.Any(curr => string.Equals(curr, currency, StringComparison.Ordinal)))
            {
                errors.Add(ErrorCodes.Spending.CurrencyIsInvalid());
            }

            return (errors.Count == 0, errors);
        }
    }
}
