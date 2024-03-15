using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses.Validation.Money
{
    internal sealed class AmountValueValidator : IValidator<Expenses.Money>
    {
        public Maybe<Error> Validate(Expenses.Money money)
        {
            return money.Amount is < Rules.Spending.MinAmount or > Rules.Spending.MaxAmount 
                ? ErrorCodes.AmountIsInvalid() 
                : Maybe<Error>.None;
        }
    }
}
