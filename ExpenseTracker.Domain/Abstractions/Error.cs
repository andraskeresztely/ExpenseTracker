using System.Collections.ObjectModel;

namespace ExpenseTracker.Domain.Abstractions
{
    public sealed record Error(string Code, string? Message = null);

    public sealed class ErrorList(IList<Error> list) : ReadOnlyCollection<Error>(list);
}
