using ExpenseTracker.Web.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ExpenseTracker.Web.App.Blazor.Expenses.Components
{
    public partial class ExpenseDetailBase : ComponentBase, IDisposable
    {
        [Parameter]
        public Func<Task<ExpenseViewModel>>? OnInitAsync { get; set; }

        [Parameter]
        public EventCallback<ExpenseViewModel> OnSaveAsync { get; set; }
        
        private EditContext? _editContext;
        private ExpenseViewModel? _expense;
        private bool _isError = false;
        private bool _isTouched = false;

        protected override async Task OnInitializedAsync()
        {
            if (OnInitAsync != null)
            {
                _expense = await OnInitAsync.Invoke();
            }

            _editContext = new EditContext(_expense!);
            _editContext.OnFieldChanged += HandleFieldChanged!;

            await Task.CompletedTask;
        }

        private async Task Save()
        {
            await OnSaveAsync.InvokeAsync(_expense);
        }

        private void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            _isTouched = true;
            _isError = !_editContext!.Validate();
            StateHasChanged();
        }

        public void Dispose()
        {
            _editContext!.OnFieldChanged -= HandleFieldChanged!;

            GC.SuppressFinalize(this);
        }
    }
}