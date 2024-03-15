using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses.Validation.ExpenseType
{
    internal sealed class ExpenseTypeValidators() : ValidatorsBase<Expenses.ExpenseType>(
    [
        (new RequiredValidator(), false),
        (new ValueValidator(), true)
    ]);
}
