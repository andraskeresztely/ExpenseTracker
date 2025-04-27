using AutoMapper;
using Confluent.Kafka;
using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;
using ExpenseTracker.Domain.Expenses.Persistence;
using Microsoft.Extensions.Options;

namespace ExpenseTracker.Persistence.Kafka.Expenses
{
    internal class ExpenseProducerConsumer(
        KafkaClient kafkaClient,
        IOptions<KafkaOptions> options,
        IMapper mapper) : IExpenseRepository
    {
        public async Task<int> CreateAsync(Expense expense)
        {
            var expenseModel = mapper.Map<ExpenseModel>(expense);

            var message = new Message<string, ExpenseModel>
            {
                Key = expenseModel.Recipient,
                Value = expenseModel
            };

            await kafkaClient.Producer.ProduceAsync(options.Value.Topic, message);
            kafkaClient.Producer.Flush(TimeSpan.FromSeconds(options.Value.TimeoutSeconds));

            return 0;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async IAsyncEnumerable<Result<Expense, Errors>> GetAllAsync()
        {
            kafkaClient.Consumer.Subscribe(options.Value.Topic);

            var expenseModel = kafkaClient.Consumer.Consume(TimeSpan.FromSeconds(options.Value.TimeoutSeconds))?.Message?.Value;

            if (expenseModel == null)
            {
                yield break;
            }

            var result = mapper.Map<Result<Expense, Errors>>(expenseModel);

            yield return await Task.FromResult(result);
        }

        public Task<Result<Expense, Errors>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Expense expense)
        {
            throw new NotImplementedException();
        }
    }
}
