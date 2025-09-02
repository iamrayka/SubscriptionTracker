using FluentAssertions;
using TransactionManagement.Core.Transactions;
using TransactionManagement.Core.Transactions.DeleteTransaction;
using TransactionManagement.Infrastructure.Transactions;

namespace TransactionManagement.IntegrationTests.Core.DeleteTransaction;

/// <summary>
/// Integration tests for DeleteTransactionCommandHandler using the real in-memory repository.
/// These tests validate the behavior of the handler when deleting existing and non-existing transactions.
/// </summary>
public class DeleteTransactionIntegrationTests
{
    /// <summary>
    /// Test: When a transaction exists, deleting it should succeed and remove it from the repository.
    /// This integration test ensures that:
    /// - The handler successfully deletes an existing transaction
    /// - The repository no longer contains the deleted transaction after the operation
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldRemoveTransaction_WhenItExists()
    {
        // Arrange: Set up a real in-memory repository and add a transaction
        var repo = new TransactionRepository(); // Using real repository implementation
        var transaction = new Transaction(Guid.NewGuid(), DateTime.Today, "To Delete", 10m);

        await repo.CreateAsync(transaction); // Prepopulate with a transaction to be deleted

        var handler = new DeleteTransactionCommandHandler(repo); // Initialize handler with real repo
        var command = new DeleteTransactionCommand { Id = transaction.Id }; // Command to delete the existing transaction

        // Act: Execute the delete handler
        var result = await handler.HandleAsync(command);

        // Assert: Ensure the result is success and transaction is removed from the repo
        result.IsSuccess.Should().BeTrue("a valid existing transaction should be deletable");

        var deleted = await repo.GetByIdAsync(transaction.Id); // Try to fetch the transaction after deletion
        deleted.Should().BeNull("the transaction should be removed from repository");
    }

    /// <summary>
    /// Test: When a transaction does not exist, the handler should return failure.
    /// This integration test ensures that:
    /// - No deletion is attempted on a missing transaction
    /// - A clear error message is returned
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldReturnFailure_WhenTransactionDoesNotExist()
    {
        // Arrange: Set up a real repository but do not add any transactions
        var repo = new TransactionRepository();
        var nonExistentId = Guid.NewGuid(); // Random ID that does not exist

        var handler = new DeleteTransactionCommandHandler(repo);
        var command = new DeleteTransactionCommand { Id = nonExistentId };

        // Act: Try to delete a transaction that was never created
        var result = await handler.HandleAsync(command);

        // Assert: Should fail and return meaningful error
        result.IsFailure.Should().BeTrue("deleting a non-existent transaction should not succeed");
        result.Error.Should().Contain("not found", "the error message should indicate the transaction was missing");
    }
}