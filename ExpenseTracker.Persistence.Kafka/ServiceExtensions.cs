using ExpenseTracker.Domain.Expenses.Persistence;
using ExpenseTracker.Persistence.Kafka.Expenses;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Persistence.Kafka
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        public static IServiceCollection AddKafka(this IServiceCollection services)
        {
            services.AddSingleton<KafkaClient>();
            services.AddTransient<IExpenseRepository, ExpenseProducerConsumer>();

            return services;
        }
    }
}
