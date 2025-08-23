using Microsoft.AspNetCore.Mvc;
using Shared.Mediator;
using SubscriptionTracker.Shared.Results;
using TransactionManagement.Core.Transactions;
using TransactionManagement.Core.Transactions.CreateTransaction;
using TransactionManagement.Core.Transactions.DeleteTransaction;
using TransactionManagement.Core.Transactions.GetTransaction;
using TransactionManagement.Core.Transactions.UpdateTransaction;

namespace TransactionManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new transaction.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransactionCommand command)
        {
            var result = await _mediator.SendAsync(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        /// <summary>
        /// Retrieves all transactions for a specific user.
        /// </summary>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var result = await _mediator.SendAsync(new GetTransactionsByUserQuery { UserId = userId });

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        /// <summary>
        /// Updates an existing transaction.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTransactionCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch between route and body.");

            var result = await _mediator.SendAsync(command);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        /// <summary>
        /// Deletes a transaction by ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.SendAsync(new DeleteTransactionCommand { Id = id });

            if (result.IsFailure)
                return NotFound(result.Error);

            return NoContent();
        }
    }
}