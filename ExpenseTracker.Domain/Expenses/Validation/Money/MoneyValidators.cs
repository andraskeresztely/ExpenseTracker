using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses.Validation.Money
{
    internal sealed class MoneyValidators() : ValidatorsBase<Expenses.Money>(
    [
        (new AmountValueValidator(), false),
        (new CurrencyRequiredValidator(), false),
        (new CurrencyValueValidator(), true)
    ]);
}
