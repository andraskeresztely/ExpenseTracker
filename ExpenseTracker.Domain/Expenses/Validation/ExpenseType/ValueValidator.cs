using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses.Validation.ExpenseType
{
    internal sealed class ValueValidator : IValidator<Expenses.ExpenseType>
    {
        public Maybe<Error> Validate(Expenses.ExpenseType expenseType)
        {
            return Rules.ExpenseType.AllTypes.Any(type => string.Equals(type, expenseType.Value, StringComparison.Ordinal))
                ? Maybe<Error>.None
                : ErrorCodes.ValueIsInvalid();
        }
    }
}
