using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Persistence.EfCore
{
    [ExcludeFromCodeCoverage]
    public sealed class EfCoreOptions
    {
        public string ConnectionString { get; set; } = string.Empty;

        public string DatabaseName { get; set; } = string.Empty;

        public bool UseInMemoryDatabase { get; set; }
    }
}