using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using ExpenseTracker.Domain.Expenses.Validation;

namespace ExpenseTracker.Persistence.EfCore.Expenses
{
    [ExcludeFromCodeCoverage]
    internal sealed class ExpenseModel
    {
        public required int Id { get; init; }

        [StringLength(Rules.Recipient.MaxRecipientLength)]
        public required string Recipient { get; init; }

        [Column(TypeName = "decimal(18,2)")]
        public required decimal SpendingAmount { get; init; }

        [StringLength(Rules.Spending.MaxCurrencyLength)]
        public required string SpendingCurrency { get; init; }

        public required DateTime TransactionDate { get; init; }

        [StringLength(Rules.ExpenseType.MaxExpenseTypeLength)]
        public required string Type { get; init; }
    }
}
