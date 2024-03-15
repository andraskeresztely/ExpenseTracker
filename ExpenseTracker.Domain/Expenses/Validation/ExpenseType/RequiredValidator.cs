using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses.Validation.ExpenseType
{
    internal sealed class RequiredValidator : IValidator<Expenses.ExpenseType>
    {
        public Maybe<Error> Validate(Expenses.ExpenseType expenseType)
        {
            return string.IsNullOrWhiteSpace(expenseType.Value) ? ErrorCodes.ValueIsRequired() : Maybe<Error>.None;
        }
    }
}
