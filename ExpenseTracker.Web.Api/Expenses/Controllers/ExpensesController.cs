using AutoMapper;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;
using ExpenseTracker.Domain.Expenses.Persistence;
using ExpenseTracker.Web.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace ExpenseTracker.Web.Api.Expenses.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class ExpensesController(
        ILogger<ExpensesController> logger,
        IMapper mapper,
        IExpenseRepository repository) : ControllerBase
    {
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateAsync(ExpenseViewModel expense)
        {
            var result = Expense.Create(
               expense.Id,
               expense.Recipient,
               expense.SpendingAmount!.Value,
               expense.SpendingCurrency,
               expense.TransactionDate!.Value,
               expense.Type);

            CheckForFailureAndThrow(result, null);

            var expenseId = await repository.InsertAsync(result.Value);

            return CreatedAtAction("Create", new { id = expenseId });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await repository.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType<ExpenseViewModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ExpenseViewModel>> GetAsync(int id)
        {
            var result = await repository.GetAsync(id);

            if (result.IsFailure)
            {
                return NotFound(new { id });
            }

            CheckForFailureAndThrow(result, id);

            return mapper.Map<ExpenseViewModel>(result.Value);
        }

        [HttpGet]
        [ProducesResponseType<IAsyncEnumerable<ExpenseViewModel>>(StatusCodes.Status200OK)]
        public async IAsyncEnumerable<ExpenseViewModel> GetAllAsync()
        {
            var results = repository.GetAllAsync();

            await foreach (var result in results)
            {
                CheckForFailureAndThrow(result, null);

                yield return mapper.Map<ExpenseViewModel>(result.Value);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType<ExpenseViewModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(int id, ExpenseViewModel expense)
        {
            if (id != expense.Id)
            {
                return BadRequest("The parameter 'id' and the entity's id must be equal.");
            }

            var result = Expense.Create(
               expense.Id,
               expense.Recipient,
               expense.SpendingAmount!.Value,
               expense.SpendingCurrency,
               expense.TransactionDate!.Value,
               expense.Type);

            CheckForFailureAndThrow(result, id);

            var updateResult = await repository.UpdateAsync(result.Value);

            return updateResult ? Ok(new { id }) : NotFound(new { id });
        }

        private void CheckForFailureAndThrow(Result<Expense> result, int? id)
        {
            if (!result.IsFailure)
            {
                return;
            }

            foreach (var error in result.Errors)
            {
                logger.LogError("Id: {id}, ErrorCode: {Code}, ErrorMessage: {Message}", id, error.Code, error.Message);
            }

            throw new InvalidOperationException("A validation error has occurred, cannot continue.");
        }
    }
}
