using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses.Validation;

namespace ExpenseTracker.Domain.Expenses
{
    public sealed class ExpenseId : Entity
    {
        public required int Value { get; init; }

        private ExpenseId() { }

        public static Result<ExpenseId, Errors> Create(int expenseId)
        {
            var (isValid, errors) = IsValid(expenseId);

            if (!isValid)
            {
                return new Errors(errors);
            }

            return new ExpenseId { Value = expenseId };
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator int(ExpenseId expenseId) => expenseId.Value;

        private static (bool IsValid, List<Error> Errors) IsValid(int expenseId)
        {
            List<Error> errors = [];

            if (expenseId < 0)
            {
                errors.Add(ErrorCodes.ExpenseId.ValueIsInvalid());
            }

            return (errors.Count == 0, errors);
        }
    }
}
