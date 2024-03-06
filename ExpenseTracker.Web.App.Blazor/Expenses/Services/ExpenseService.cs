using ExpenseTracker.Web.Model;
using System.Net.Http.Json;

namespace ExpenseTracker.Web.App.Blazor.Expenses.Services
{
    internal sealed class ExpenseService(HttpClient httpClient)
    {
        private HttpClient HttpClient { get; } = httpClient;

        public async Task CreateAsync(ExpenseViewModel expense)
        {
            await HttpClient.PostAsJsonAsync("expenses", expense);
        }

        public async Task DeleteAsync(int id)
        {
            await HttpClient.DeleteAsync($"expenses/{id}");
        }

        public async Task<ExpenseViewModel?> GetAsync(int id)
        {
            return await HttpClient.GetFromJsonAsync<ExpenseViewModel>($"expenses/{id}")!;
        }

        public IAsyncEnumerable<ExpenseViewModel?> GetAllAsync()
        {
            return HttpClient.GetFromJsonAsAsyncEnumerable<ExpenseViewModel>("expenses");
        }

        public async Task UpdateAsync(int id, ExpenseViewModel expense)
        {
            await HttpClient.PutAsJsonAsync($"expenses/{id}", expense);
        }
    }
}
