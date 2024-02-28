using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses.Validation;

namespace ExpenseTracker.Domain.Expenses
{
    public sealed record Money
    {
        public required decimal Amount { get; init; }
        public required string Currency { get; init; }

        public static Result<Money> Create(decimal amount, string currency)
        {
            var (isValid, errors) = IsValid(amount, currency);
            if (!isValid)
            {
                return new ErrorList(errors);
            }

            return new Money { Amount = amount, Currency = currency };
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
                errors.Add(Errors.Spending.AmountIsInvalid());
            }

            if (string.IsNullOrWhiteSpace(currency))
            {
                errors.Add(Errors.Spending.CurrencyIsRequired());
            }
            else if (!Rules.Spending.AllCurrencies.Any(curr => string.Equals(curr, currency, StringComparison.Ordinal)))
            {
                errors.Add(Errors.Spending.CurrencyIsInvalid());
            }

            return (errors.Count == 0, errors);
        }
    }
}
