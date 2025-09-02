using Shared.Mediator;
using SubscriptionTracker.Shared.Results;
using TransactionManagement.Core.Transactions;

namespace TransactionManagement.Core.Transactions.CreateTransaction;

/// <summary>
/// Handles the creation of a new transaction.
/// </summary>
public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Result<Transaction>>
{
    private readonly ITransactionRepository _repository;

    public CreateTransactionCommandHandler(ITransactionRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    /// <summary>
    /// Handles the creation logic and saves the transaction.
    /// </summary>
    public async Task<Result<Transaction>> HandleAsync(CreateTransactionCommand request)
    {
        if (request.UserId == Guid.Empty)
            return Result<Transaction>.Failure("User ID cannot be empty.");

        try
        {
            var transaction = new Transaction(
                request.UserId,
                request.TransactionDate,
                request.Description,
                request.Amount
            );

            await _repository.CreateAsync(transaction);
            return Result<Transaction>.Success(transaction);
        }
        catch (ArgumentException ex)
        {
            Console.Error.WriteLine(ex); // Or use ILogger if available
            return Result<Transaction>.Failure("Invalid input provided.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex); // internal only
            return Result<Transaction>.Failure("Unexpected error while creating transaction.");
        }
    }
}
