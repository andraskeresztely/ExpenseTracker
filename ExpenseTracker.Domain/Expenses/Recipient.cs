using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses.Validation;

namespace ExpenseTracker.Domain.Expenses
{
    public sealed class Recipient : Entity
    {
        public required string Name { get; init; }

        private Recipient() { }

        public static Result<Recipient, Errors> Create(string name)
        {
            var (isValid, errors) = IsValid(name);

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

        private static (bool IsValid, List<Error> Errors) IsValid(string name)
        {
            List<Error> errors = [];

            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add(ErrorCodes.Recipient.NameIsRequired());
            }
            else if (name.Length is < Rules.Recipient.MinRecipientLength or > Rules.Recipient.MaxRecipientLength)
            {
                errors.Add(ErrorCodes.Recipient.LengthIsInvalid());
            }

            return (errors.Count == 0, errors);
        }
    }
}
