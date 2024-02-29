using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses.Validation;

namespace ExpenseTracker.Domain.Expenses
{
    public sealed class ExpenseType : ValueObject
    {
        public required string Value { get; init; }

        private ExpenseType() {}

        public static Result<ExpenseType, Errors> Create(string expenseType)
        {
            var (isValid, errors) = IsValid(expenseType);

            if (!isValid)
            {
                return new Errors(errors);
            }

            return new ExpenseType { Value = expenseType };
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }

        private static (bool IsValid, List<Error> Errors) IsValid(string expenseType)
        {
            List<Error> errors = [];

            if (string.IsNullOrWhiteSpace(expenseType))
            {
                errors.Add(ErrorCodes.ExpenseType.ValueIsRequired());
            }
            else
            {
                if (!Rules.ExpenseType.AllTypes.Any(type => string.Equals(type, expenseType, StringComparison.Ordinal)))
                {
                    errors.Add(ErrorCodes.ExpenseType.ValueIsInvalid());
                }
            }

            return (errors.Count == 0, errors);
        }
    }
}
