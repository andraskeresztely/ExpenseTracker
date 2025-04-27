using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses.Validation.Money;

namespace ExpenseTracker.Domain.Expenses
{
    public sealed class Money : ComparableValueObject
    {
        public required decimal Amount { get; init; }
        public required string Currency { get; init; }

        private Money() { }

        private static readonly MoneyValidators Validators = new();

        public static Result<Money, Errors> Create(decimal amount, string currency)
        {
            var (isValid, errors) = Validators.AreValid(new Money { Amount = amount, Currency = currency });

            if (!isValid)
            {
                return new Errors(errors);
            }

            return new Money { Amount = amount, Currency = currency };
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        public override string ToString()
        {
            return $"{Currency} {Amount}";
        }
    }
}
