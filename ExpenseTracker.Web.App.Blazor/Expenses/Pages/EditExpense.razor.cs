using ExpenseTracker.Web.App.Blazor.Expenses.Services;
using ExpenseTracker.Web.Model;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Web.App.Blazor.Expenses.Pages
{
    public partial class EditExpense
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private ExpenseService ExpenseService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private async Task<ExpenseViewModel> InitAsync()
        {
            var serviceExpense = await ExpenseService.GetAsync(Id);

            return serviceExpense!;
        }

        private async Task SaveAsync(ExpenseViewModel expense)
        {
            await ExpenseService.UpdateAsync(Id, expense);

            NavigationManager.NavigateTo("/");
        }
    }
}
