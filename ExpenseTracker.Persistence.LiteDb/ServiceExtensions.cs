using ExpenseTracker.Domain.Expenses.Persistence;
using ExpenseTracker.Persistence.LiteDb.Expenses;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Persistence.LiteDb
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        public static IServiceCollection AddLiteDb(this IServiceCollection services)
        {
            services.AddSingleton<ILiteDatabase>(provider =>
            {
                var options = provider.GetService<IOptions<LiteDbOptions>>();

                return new LiteDatabase(options!.Value.DatabasePath);
            });

            services.AddSingleton<IExpenseRepository, ExpenseRepository>();

            return services;
        }
    }
}
