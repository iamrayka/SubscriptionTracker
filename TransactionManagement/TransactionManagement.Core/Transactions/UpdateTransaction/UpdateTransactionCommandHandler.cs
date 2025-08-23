using Shared.Mediator;
using SubscriptionTracker.Shared.Results;
using TransactionManagement.Core.Transactions;

namespace TransactionManagement.Core.Transactions.UpdateTransaction;

/// <summary>
/// Handles the update of an existing transaction.
/// </summary>
public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, Result<Transaction>>
{
    private readonly ITransactionRepository _repository;

    public UpdateTransactionCommandHandler(ITransactionRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /// <summary>
    /// Executes the update and persists the modified transaction.
    /// </summary>
    /// <param name="request">The command containing updated transaction data.</param>
    public async Task<Result<Transaction>> HandleAsync(UpdateTransactionCommand request)
    {
        if (request.Id == Guid.Empty)
            return Result<Transaction>.Failure("Transaction ID cannot be empty.");

        var transaction = await _repository.GetByIdAsync(request.Id);
        if (transaction == null)
            return Result<Transaction>.Failure("Transaction not found.");

        try
        {
            transaction.Update(request.TransactionDate, request.Description, request.Amount);
            await _repository.UpdateAsync(transaction);
            return Result<Transaction>.Success(transaction);
        }
        catch (Exception ex)
        {
            return Result<Transaction>.Failure($"Failed to update transaction: {ex.Message}");
        }
    }
}
