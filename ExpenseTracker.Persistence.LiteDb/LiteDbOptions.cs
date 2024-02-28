using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Persistence.LiteDb
{
    [ExcludeFromCodeCoverage]
    public sealed class LiteDbOptions
    {
        public string DatabasePath { get; set; } = string.Empty;
    }
}