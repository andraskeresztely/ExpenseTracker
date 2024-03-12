using Asp.Versioning;
using AutoMapper;
using CSharpFunctionalExtensions;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Expenses;
using ExpenseTracker.Domain.Expenses.Persistence;
using ExpenseTracker.Web.Model;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Api.Expenses.Controllers
{
    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/expenses")]
    public sealed class ExpensesController(
        ILogger<ExpensesController> logger,
        IMapper mapper,
        IExpenseRepository repository) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(ExpenseViewModel expense)
        {
            var result = mapper.Map<Result<Expense, Errors>>(expense);

            CheckForFailureAndThrow(result, null);

            var expenseId = await repository.CreateAsync(result.Value);

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
                var details = new ProblemDetails
                {
                    Extensions = { { "id", id } },
                    Status = StatusCodes.Status404NotFound,
                    Title = "Expense not found.",
                };

                return NotFound(details);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(int id, ExpenseViewModel expense)
        {
            if (id != expense.Id)
            {
                var badRequestDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "The parameter 'id' and the entity's id must be equal."
                };

                return BadRequest(badRequestDetails);
            }

            var result = mapper.Map<Result<Expense, Errors>>(expense);

            CheckForFailureAndThrow(result, id);

            var updateResult = await repository.UpdateAsync(result.Value);

            if (updateResult)
            {
                return Ok(new { id });
            }

            var notFoundDetails = new ProblemDetails
            {
                Extensions = { { "id", id } },
                Status = StatusCodes.Status404NotFound,
                Title = "Expense not found."
            };

            return NotFound(notFoundDetails);
        }

        private void CheckForFailureAndThrow(Result<Expense, Errors> result, int? id)
        {
            if (!result.IsFailure)
            {
                return;
            }

            foreach (var error in result.Error)
            {
                logger.LogError("Id: {id}, ErrorCode: {Code}, ErrorMessage: {Message}", id, error.Code, error.Message);
            }

            throw new InvalidOperationException("A validation error has occurred, cannot continue.");
        }
    }
}
