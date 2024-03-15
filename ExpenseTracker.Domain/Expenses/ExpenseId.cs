using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses.Validation.ExpenseId;

namespace ExpenseTracker.Domain.Expenses
{
    public sealed class ExpenseId : Entity
    {
        public required int Value { get; init; }

        private ExpenseId() { }

        private static readonly ExpenseIdValidators Validators = new();

        public static Result<ExpenseId, Errors> Create(int expenseId)
        {
            var (isValid, errors) = Validators.AreValid(new ExpenseId { Value = expenseId });

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
    }
}
