using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses.Validation.ExpenseId
{
    internal sealed class ExpenseIdValidators() : ValidatorsBase<Expenses.ExpenseId>(
    [
        (new ValueValidator(), false)
    ]);
}
