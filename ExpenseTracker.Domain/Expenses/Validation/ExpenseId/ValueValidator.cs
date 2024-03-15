using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses.Validation.ExpenseId
{
    internal sealed class ValueValidator : IValidator<Expenses.ExpenseId>
    {
        public Maybe<Error> Validate(Expenses.ExpenseId expenseId)
        {
            return expenseId.Value < 0 ? ErrorCodes.ValueIsInvalid() : Maybe<Error>.None;
        }
    }
}
