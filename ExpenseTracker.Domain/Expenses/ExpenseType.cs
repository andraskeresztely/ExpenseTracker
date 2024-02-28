using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses.Validation;

namespace ExpenseTracker.Domain.Expenses
{
    public sealed record ExpenseType
    {
        public required string Value { get; init; }

        private ExpenseType() {}

        public static Result<ExpenseType> Create(string expenseType)
        {
            var (isValid, errors) = IsValid(expenseType);
            if (!isValid)
            {
                return new ErrorList(errors);
            }

            return new ExpenseType { Value = expenseType };
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
                errors.Add(Errors.ExpenseType.ValueIsRequired());
            }
            else
            {
                if (!Rules.ExpenseType.AllTypes.Any(type => string.Equals(type, expenseType, StringComparison.Ordinal)))
                {
                    errors.Add(Errors.ExpenseType.ValueIsInvalid());
                }
            }

            return (errors.Count == 0, errors);
        }
    }
}
