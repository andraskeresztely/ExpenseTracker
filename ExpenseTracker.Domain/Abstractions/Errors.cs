using CSharpFunctionalExtensions;
using System.Collections.ObjectModel;

namespace ExpenseTracker.Domain.Abstractions
{
    public sealed class Errors(IEnumerable<Error> errors) : ReadOnlyCollection<Error>(errors.ToList()), ICombine
    {
        public ICombine Combine(ICombine value)
        {
            var errors = Items.ToList();

            errors.AddRange(((Errors)value).Items);

            return new Errors(errors);
        }
    }
}