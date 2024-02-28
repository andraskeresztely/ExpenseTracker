using ExpenseTracker.Web.App.Blazor.Expenses.Services;
using ExpenseTracker.Web.Model;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Web.App.Blazor.Expenses.Pages
{
    public partial class NewExpense
    {
        [Inject]
        private ExpenseService ExpenseService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private async Task<ExpenseViewModel> InitAsync()
        {
            var expenseViewModel = new ExpenseViewModel
            {
                Recipient = string.Empty,
                SpendingAmount = null,
                SpendingCurrency = string.Empty,
                TransactionDate = DateTime.Now.Date,
                Type = string.Empty
            };

            return await Task.FromResult(expenseViewModel);
        }

        private async Task SaveAsync(ExpenseViewModel expense)
        {
            await ExpenseService.CreateAsync(expense);

            NavigationManager.NavigateTo("/");
        }
    }
}