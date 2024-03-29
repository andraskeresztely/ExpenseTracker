﻿using ExpenseTracker.Domain.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Domain.Expenses.Validation.ExpenseId
{
    [ExcludeFromCodeCoverage]
    internal static class ErrorCodes
    {
        public static Error ValueIsInvalid() => new("expense.id.value.invalid", "Value is invalid.");
    }
}
