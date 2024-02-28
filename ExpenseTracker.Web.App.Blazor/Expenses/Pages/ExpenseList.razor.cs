using ExpenseTracker.Web.App.Blazor.Components;
using ExpenseTracker.Web.App.Blazor.Expenses.Services;
using ExpenseTracker.Web.Model;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ExpenseTracker.Web.App.Blazor.Expenses.Pages
{
    public partial class ExpenseList
    {
        [Inject]
        private IDialogService DialogService { get; set; } = default!;

        [Inject]
        private ExpenseService ExpenseService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private List<ExpenseViewModel>? _expenses;
        private bool _loading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadItems();
        }

        private void EditExpense(int id)
        {
            NavigationManager.NavigateTo($"/edit/{id}");
        }

        private async Task LoadItems()
        {
            _loading = true;
            _expenses = [];

            var serviceExpenses = ExpenseService.GetAllAsync();

            await foreach (var expense in serviceExpenses)
            {
                _expenses.Add(expense!);
                StateHasChanged();
            }

            _loading = false;
        }

        private async Task OpenConfirmDialog(int id)
        {
            var parameters = new DialogParameters<ConfirmDialog>
            {
                { dialog => dialog.ContentText, "Do you really want to delete this expense? This process cannot be undone." },
                { dialog => dialog.ButtonText, "Delete" },
                { dialog => dialog.Color, Color.Secondary }
            };

            var options = new DialogOptions { CloseOnEscapeKey = true, CloseButton = true, MaxWidth = MaxWidth.ExtraSmall};

            var dialog = await DialogService.ShowAsync<ConfirmDialog>("Are you sure?", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await ExpenseService.DeleteAsync(id);
                await LoadItems();
            }
        }
    }
}
