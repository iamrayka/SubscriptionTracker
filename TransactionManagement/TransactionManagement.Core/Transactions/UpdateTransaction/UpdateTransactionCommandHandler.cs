using Shared.Mediator;
using SubscriptionTracker.Shared.Results;

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
            // Log the detailed error internally for developers
            // (e.g., using Serilog, NLog, etc.)
            Console.Error.WriteLine(ex); // Placeholder

            // Return a generic, user-safe error message
            return Result<Transaction>.Failure("An unexpected error occurred while updating the transaction.");
        }
    }
}
