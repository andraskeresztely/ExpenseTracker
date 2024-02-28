using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ExpenseTracker.Web.App.Blazor.Components
{
    public partial class ConfirmDialog
    {
        [CascadingParameter] private MudDialogInstance? Dialog { get; set; }

        [Parameter] public string? ContentText { get; set; }

        [Parameter] public string? ButtonText { get; set; }

        [Parameter] public Color Color { get; set; }

        private void Submit() => Dialog!.Close(DialogResult.Ok(true));
        private void Cancel() => Dialog!.Cancel();
    }
}
