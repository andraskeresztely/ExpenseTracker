using ExpenseTracker.Domain.Expenses.Validation;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Web.Model
{
    [ExcludeFromCodeCoverage]
    public sealed class ExpenseViewModel
    {
        public int Id { get; set; }

        [Required,
         MinLength(Rules.Recipient.MinRecipientLength,
            ErrorMessage = "Please use a Recipient with more than 2 letters."),
         MaxLength(Rules.Recipient.MaxRecipientLength,
            ErrorMessage = "Please use a Recipient with less than 255 letters.")]
        public required string Recipient { get; set; }

        [Required,
         Range(Rules.Spending.MinAmount, Rules.Spending.MaxAmount,
            ErrorMessage = "Please use an Amount between 1 and 1.000.000.")]
        public required decimal? SpendingAmount { get; set; }

        [Required,
         AllowedValues(Rules.Spending.CHF, Rules.Spending.EUR, Rules.Spending.USD,
            ErrorMessage = "Please use a Currency of CHF, EUR or USD.")]
        public required string SpendingCurrency { get; set; }

        [Required]
        public required DateTime? TransactionDate { get; set; }

        [Required,
         AllowedValues(Rules.ExpenseType.Food, Rules.ExpenseType.Drinks, Rules.ExpenseType.Other,
            ErrorMessage = "Please use an Expense type of Food, Drinks or Other.")]
        public required string Type { get; set; }

        public static IReadOnlyCollection<string> Currencies => Rules.Spending.AllCurrencies;

        public static DateTime? MaxTransactionDate => Rules.TransactionDate.MaxTransactionDate;

        public static IReadOnlyCollection<string> Types => Rules.ExpenseType.AllTypes;
    }
}
