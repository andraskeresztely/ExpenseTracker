using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Persistence.EfCore.Expenses
{
    [ExcludeFromCodeCoverage]
    internal sealed class ExpenseContext(DbContextOptions<ExpenseContext> options) : DbContext(options)
    {
        public DbSet<ExpenseModel> Expenses { get; set; } = null!;
    }
}