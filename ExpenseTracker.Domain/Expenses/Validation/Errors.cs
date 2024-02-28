using ExpenseTracker.Domain.Abstractions;

namespace ExpenseTracker.Domain.Expenses.Validation
{
    public static class Errors
    {
        public static class ExpenseType
        {
            public static Error ValueIsInvalid() => new("expense.type.value.invalid", "Value is invalid.");

            public static Error ValueIsRequired() => new("expense.type.value.required", "Value is required.");
        }

        public static class Recipient
        {
            public static Error LengthIsInvalid() => new("recipient.length.invalid", "Length is invalid.");

            public static Error ValueIsRequired() => new("recipient.value.required", "Value is required.");
        }

        public static class Spending
        {
            public static Error AmountIsInvalid() => new("spending.amount.invalid", "Spending amount is invalid.");

            public static Error CurrencyIsInvalid() => new("spending.currency.invalid", "Spending currency is invalid.");

            public static Error CurrencyIsRequired() => new("spending.currency.required", "Spending currency is required.");
        }

        public static class TransactionDate
        {
            public static Error ValueIsInvalid() => new("transaction.date.value.invalid", "Value is invalid.");

            public static Error ValueIsRequired() => new("transaction.date.value.required", "Value is required.");
        }
    }
}
