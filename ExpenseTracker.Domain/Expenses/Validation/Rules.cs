namespace ExpenseTracker.Domain.Expenses.Validation
{
    public static class Rules
    {
        public static class ExpenseType
        {
            public const string Food = "Food";
            public const string Drinks = "Drinks";
            public const string Other = "Other";
            public static readonly IReadOnlyCollection<string> AllTypes = [Food, Drinks, Other];
        }

        public static class Recipient
        {
            public const int MinRecipientLength = 2;
            public const int MaxRecipientLength = 255;
        }

        public static class Spending
        {
            public const int MinAmount = 1;
            public const int MaxAmount = 1_000_000;

            public const string CHF = nameof(CHF);
            public const string EUR = nameof(EUR);
            public const string USD = nameof(USD);
            public static readonly IReadOnlyCollection<string> AllCurrencies = [CHF, EUR, USD];
        }

        public static class TransactionDate
        {
            public static DateTime MaxTransactionDate => DateTime.Today;
        }
    }
}
