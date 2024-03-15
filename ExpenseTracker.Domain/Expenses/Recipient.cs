using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses.Validation.Recipient;

namespace ExpenseTracker.Domain.Expenses
{
    public sealed class Recipient : Entity
    {
        public required string Name { get; init; }

        private Recipient() { }

        private static readonly RecipientValidators Validators = new();

        public static Result<Recipient, Errors> Create(string name)
        {
            var (isValid, errors) = Validators.AreValid(new Recipient { Name = name });

            if (!isValid)
            {
                return new Errors(errors);
            }

            return new Recipient { Name = name };
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
