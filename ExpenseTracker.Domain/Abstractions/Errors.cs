using System.Collections.ObjectModel;
using CSharpFunctionalExtensions;

namespace ExpenseTracker.Domain.Abstractions
{
    public sealed class Errors(IList<Error> errors) : ReadOnlyCollection<Error>(errors), ICombine
    {
        public ICombine Combine(ICombine value)
        {
            var errors = Items.ToList();

            errors.AddRange(((Errors)value).Items);

            return new Errors(errors);
        }
    }
}