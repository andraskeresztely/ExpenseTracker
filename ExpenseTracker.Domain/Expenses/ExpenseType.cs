using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses.Validation.ExpenseType;

namespace ExpenseTracker.Domain.Expenses
{
    public sealed class ExpenseType : ComparableValueObject
    {
        public required string Value { get; init; }

        private ExpenseType() {}

        private static readonly ExpenseTypeValidators Validators = new();

        public static Result<ExpenseType, Errors> Create(string expenseType)
        {
            var (isValid, errors) = Validators.AreValid(new ExpenseType { Value = expenseType });

            if (!isValid)
            {
                return new Errors(errors);
            }

            return new ExpenseType { Value = expenseType };
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
