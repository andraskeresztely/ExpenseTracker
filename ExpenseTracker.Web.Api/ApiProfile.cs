using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using ExpenseTracker.Domain.Expenses;
using ExpenseTracker.Web.Model;

namespace ExpenseTracker.Web.Api
{
    [ExcludeFromCodeCoverage]
    public sealed class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<Expense, ExpenseViewModel>();
        }
    }
}
