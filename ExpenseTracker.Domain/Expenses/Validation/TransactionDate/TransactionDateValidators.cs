using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses.Validation.TransactionDate
{
    internal sealed class TransactionDateValidators() : ValidatorsBase<Expenses.TransactionDate>(
    [
        (new RequiredValidator(), false),
        (new ValueValidator(), true)
    ]);
}
