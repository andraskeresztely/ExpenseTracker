using ExpenseTracker.Web.Model;
using System.Net.Http.Json;

namespace ExpenseTracker.Web.App.Blazor.Expenses.Services
{
    internal sealed class ExpenseService(HttpClient httpClient)
    {
        private HttpClient HttpClient { get; } = httpClient;

        public async Task CreateAsync(ExpenseViewModel expense)
        {
            await HttpClient.PostAsJsonAsync("Expenses", expense);
        }

        public async Task DeleteAsync(int id)
        {
            await HttpClient.DeleteAsync($"Expenses/{id}");
        }

        public async Task<ExpenseViewModel?> GetAsync(int id)
        {
            return await HttpClient.GetFromJsonAsync<ExpenseViewModel>($"Expenses/{id}")!;
        }

        public IAsyncEnumerable<ExpenseViewModel?> GetAllAsync()
        {
            return HttpClient.GetFromJsonAsAsyncEnumerable<ExpenseViewModel>("Expenses");
        }

        public async Task UpdateAsync(int id, ExpenseViewModel expense)
        {
            await HttpClient.PutAsJsonAsync($"Expenses/{id}", expense);
        }
    }
}
