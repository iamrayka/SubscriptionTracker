using Microsoft.AspNetCore.Mvc;
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
        private readonly CreateTransactionHandler _createHandler;
        private readonly GetTransactionsByUserHandler _getHandler;
        private readonly UpdateTransactionHandler _updateHandler;
        private readonly DeleteTransactionHandler _deleteHandler;

        public TransactionsController(CreateTransactionHandler createHandler,
                                      GetTransactionsByUserHandler getHandler,
                                      UpdateTransactionHandler updateHandler,
                                      DeleteTransactionHandler deleteHandler)

        {
            _createHandler = createHandler;
            _getHandler = getHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransactionRequest request)
        {
            var transaction = await _createHandler.HandleAsync(
                request.UserId,
                request.TransactionDate,
                request.Description,
                request.Amount
            );

            return Ok(transaction);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var transactions = await _getHandler.HandleAsync(userId);
            return Ok(transactions);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTransactionRequest request)
        {
            var transaction = await _updateHandler.HandleAsync(id, request);
            if (transaction == null) return NotFound();
            return Ok(transaction);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _deleteHandler.HandleAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

    }
}
