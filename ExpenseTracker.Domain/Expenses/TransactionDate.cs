using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses.Validation;

namespace ExpenseTracker.Domain.Expenses
{
    public sealed class TransactionDate : ValueObject
    {
        public required DateTime Value { get; init; }

        private TransactionDate() {}

        public static Result<TransactionDate, Errors> Create(DateTime transactionDate)
        {
            var (isValid, errors) = IsValid(transactionDate);

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

        private static (bool IsValid, List<Error> Errors) IsValid(DateTime transactionDate)
        {
            List<Error> errors = [];

            if (transactionDate == DateTime.MinValue)
            {
                errors.Add(ErrorCodes.TransactionDate.ValueIsRequired());
            }
            else if (transactionDate.Date > Rules.TransactionDate.MaxTransactionDate.Date)
            {
                errors.Add(ErrorCodes.TransactionDate.ValueIsInvalid());
            }

            return (errors.Count == 0, errors);
        }
    }
}
