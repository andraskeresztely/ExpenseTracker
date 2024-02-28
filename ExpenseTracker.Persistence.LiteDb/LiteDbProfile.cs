using AutoMapper;
using ExpenseTracker.Domain.Expenses;
using ExpenseTracker.Persistence.LiteDb.Expenses;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Persistence.LiteDb
{
    [ExcludeFromCodeCoverage]
    public sealed class LiteDbProfile : Profile
    {
        public LiteDbProfile()
        {
            CreateMap<Expense, ExpenseModel>();
        }
    }
}
