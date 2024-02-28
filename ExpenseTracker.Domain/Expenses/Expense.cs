using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses.Validation;

namespace ExpenseTracker.Domain.Expenses
{
    public sealed class Expense
    {
        public required int Id { get; init; }
        public required string Recipient { get; init; }
        public required Money Spending { get; init; }
        public required DateTime TransactionDate { get; init; }
        public required ExpenseType Type { get; init; }

        private Expense() {}

        public static Result<Expense> Create(
            int id,
            string recipient,
            decimal spendingAmount,
            string spendingCurrency,
            DateTime transactionDate,
            string expenseType)
        {
            var (isValid, errors) = IsValid(recipient, spendingAmount, spendingCurrency, transactionDate, expenseType);
            if (!isValid)
            {
                return new ErrorList(errors);
            }

            var spending = Money.Create(spendingAmount, spendingCurrency);
            var expenseTypeResult = ExpenseType.Create(expenseType);

            return new Expense 
            { 
                Id = id,
                Recipient = recipient, 
                Spending = spending.Value, 
                TransactionDate = transactionDate, 
                Type = expenseTypeResult.Value 
            };
        }

        private static (bool IsValid, List<Error> Errors) IsValid(
            string recipient,
            decimal spendingAmount,
            string spendingCurrency,
            DateTime transactionDate,
            string expenseType)
        {
            List<Error> errors = [];

            if (string.IsNullOrWhiteSpace(recipient))
            {
                errors.Add(Errors.Recipient.ValueIsRequired());
            }
            else if (recipient.Length is < Rules.Recipient.MinRecipientLength or > Rules.Recipient.MaxRecipientLength)
            {
                errors.Add(Errors.Recipient.LengthIsInvalid());
            }

            var moneyResult = Money.Create(spendingAmount, spendingCurrency);
            if (moneyResult.IsFailure)
            {
                errors.AddRange(moneyResult.Errors);
            }

            if (transactionDate == DateTime.MinValue)
            {
                errors.Add(Errors.TransactionDate.ValueIsRequired());
            }
            else if (transactionDate.Date > Rules.TransactionDate.MaxTransactionDate.Date)
            {
                errors.Add(Errors.TransactionDate.ValueIsInvalid());
            }

            var expenseTypeResult = ExpenseType.Create(expenseType);
            if (expenseTypeResult.IsFailure)
            {
                errors.AddRange(expenseTypeResult.Errors);
            }

            return (errors.Count == 0, errors);
        }
    }
}
