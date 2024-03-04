using ExpenseTracker.Web.App.Blazor.Expenses.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Web.App.Blazor
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.Configuration["Settings:ApiBaseUrl"]!) });
            builder.Services.AddScoped<ExpenseService>();
            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}
