using CSharpFunctionalExtensions;

namespace ExpenseTracker.Domain.Abstractions
{
    internal interface IValidator<in T>
    {
        Maybe<Error> Validate(T entity);
    }
}
