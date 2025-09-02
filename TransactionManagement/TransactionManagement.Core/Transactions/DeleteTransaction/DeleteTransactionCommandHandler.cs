using Shared.Mediator;
using SubscriptionTracker.Shared.Results;

namespace TransactionManagement.Core.Transactions.DeleteTransaction;

/// <summary>
/// Handles the deletion of a transaction.
/// </summary>
public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, Result>
{
    private readonly ITransactionRepository _repository;

    public DeleteTransactionCommandHandler(ITransactionRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /// <summary>
    /// Attempts to delete the specified transaction.
    /// </summary>
    /// <param name="request">The delete command containing the transaction ID.</param>
    /// <returns>A Result indicating success or failure.</returns>
    public async Task<Result> HandleAsync(DeleteTransactionCommand request)
    {
        if (request.Id == Guid.Empty)
            return Result.Failure("Transaction ID cannot be empty.");

        try
        {
            var transaction = await _repository.GetByIdAsync(request.Id);
            if (transaction == null)
                return Result.Failure($"Transaction with ID '{request.Id}' was not found.");

            await _repository.DeleteAsync(request.Id);
            return Result.Success();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex); // or use ILogger
            return Result.Failure("An unexpected error occurred while deleting the transaction.");
        }
    }
}