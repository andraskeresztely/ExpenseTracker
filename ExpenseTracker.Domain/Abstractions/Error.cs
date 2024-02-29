namespace ExpenseTracker.Domain.Abstractions
{
    public sealed record Error(string Code, string? Message = null);
}
