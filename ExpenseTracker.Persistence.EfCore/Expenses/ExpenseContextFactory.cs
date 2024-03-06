using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Persistence.EfCore.Expenses;

[ExcludeFromCodeCoverage]
internal sealed class ExpenseContextFactory : IDesignTimeDbContextFactory<ExpenseContext>
{
    public ExpenseContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ExpenseContext>();
        optionsBuilder.UseSqlServer();

        return new ExpenseContext(optionsBuilder.Options);
    }
}