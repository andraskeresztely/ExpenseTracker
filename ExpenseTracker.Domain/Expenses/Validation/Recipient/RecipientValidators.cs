using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses.Validation.Recipient
{
    internal sealed class RecipientValidators() : ValidatorsBase<Expenses.Recipient>(
    [
        (new RequiredValidator(), false),
        (new ValueValidator(), true)
    ]);
}
