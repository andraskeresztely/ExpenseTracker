using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses.Validation.TransactionDate;

namespace ExpenseTracker.Domain.Expenses
{
    public sealed class TransactionDate : ValueObject
    {
        public required DateTime Value { get; init; }

        private TransactionDate() {}

        private static readonly TransactionDateValidators Validators = new();

        public static Result<TransactionDate, Errors> Create(DateTime transactionDate)
        {
            var (isValid, errors) = Validators.AreValid(new TransactionDate { Value = transactionDate });

            if (!isValid)
            {
                return new Errors(errors);
            }

            return new TransactionDate { Value = transactionDate };
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value.ToString("dd/MM/yyyy");
        }

        public static implicit operator DateTime(TransactionDate transactionDate) => transactionDate.Value;
    }
}
