using CSharpFunctionalExtensions;

namespace ExpenseTracker.Domain.Abstractions
{
    internal interface IValidator<in T>
    {
        public Maybe<Error> Validate(T entity);
    }
}
