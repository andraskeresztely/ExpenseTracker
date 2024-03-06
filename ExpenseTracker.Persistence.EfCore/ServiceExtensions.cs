using ExpenseTracker.Domain.Expenses.Persistence;
using ExpenseTracker.Persistence.EfCore.Expenses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Persistence.EfCore
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        public static IServiceCollection AddEfCore(this IServiceCollection services)
        {
            services.AddSingleton<IExpenseRepository, ExpenseRepository>();

            services.AddSingleton(provider =>
            {
                var options = provider.GetService<IOptions<EfCoreOptions>>();

                return options!.Value.UseInMemoryDatabase
                    ? new DbContextOptionsBuilder<ExpenseContext>().UseInMemoryDatabase(options.Value.DatabaseName).Options
                    : new DbContextOptionsBuilder<ExpenseContext>().UseSqlServer(options.Value.ConnectionString).Options;
            });

            return services;
        }
    }
}
