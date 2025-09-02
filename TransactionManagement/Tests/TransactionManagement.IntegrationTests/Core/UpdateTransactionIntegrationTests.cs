using FluentAssertions;
using TransactionManagement.Core.Transactions;
using TransactionManagement.Core.Transactions.UpdateTransaction;
using TransactionManagement.Infrastructure.Transactions;

namespace TransactionManagement.IntegrationTests.Core.UpdateTransaction;

/// <summary>
/// Integration tests for UpdateTransactionCommandHandler using the real in-memory repository.
/// These tests cover successful updates as well as validation and existence failures.
/// </summary>
public class UpdateTransactionIntegrationTests
{
    /// <summary>
    /// Test: A valid update command should update the transaction in the repository.
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldUpdateExistingTransaction_WhenValid()
    {
        // Arrange: Create an initial transaction and persist it in the in-memory repository
        var repo = new TransactionRepository();
        var original = new Transaction(Guid.NewGuid(), DateTime.Today, "Original", 50);
        await repo.CreateAsync(original);

        // Create a handler and a valid update command
        var handler = new UpdateTransactionCommandHandler(repo);
        var command = new UpdateTransactionCommand
        {
            Id = original.Id,
            TransactionDate = DateTime.Today.AddDays(1),
            Description = "Updated Desc",
            Amount = 99.99m
        };

        // Act: Execute the handler to update the transaction
        var result = await handler.HandleAsync(command);

        // Assert: Verify the result is successful and values were updated
        result.IsSuccess.Should().BeTrue("the transaction exists and the update data is valid");
        result.Value.Should().NotBeNull("the updated transaction should be returned");
        result.Value.Description.Should().Be("Updated Desc", "the description should reflect the updated value");

        // Assert: Check that the changes persisted in the repository
        var updated = await repo.GetByIdAsync(original.Id);
        updated!.Amount.Should().Be(99.99m, "the amount should be updated in the repository");
    }

    /// <summary>
    /// Test: Updating with an empty GUID should fail validation.
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldReturnFailure_WhenTransactionIdIsEmpty()
    {
        // Arrange: Real handler with in-memory repository
        var repo = new TransactionRepository();
        var handler = new UpdateTransactionCommandHandler(repo);

        var command = new UpdateTransactionCommand
        {
            Id = Guid.Empty, // Invalid input
            TransactionDate = DateTime.Today,
            Description = "Doesn't matter",
            Amount = 10
        };

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsFailure.Should().BeTrue("empty GUID is invalid");
        result.Error.Should().Contain("Transaction ID", "error should mention the invalid field");
    }

    /// <summary>
    /// Test: Attempting to update a transaction that doesn't exist should fail.
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldReturnFailure_WhenTransactionNotFound()
    {
        // Arrange: Use a GUID not present in the repository
        var repo = new TransactionRepository();
        var handler = new UpdateTransactionCommandHandler(repo);

        var command = new UpdateTransactionCommand
        {
            Id = Guid.NewGuid(), // Non-existent ID
            TransactionDate = DateTime.Today,
            Description = "Missing",
            Amount = 100
        };

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsFailure.Should().BeTrue("the transaction ID doesn't exist in the repo");
        result.Error.Should().Contain("not found", "error message should indicate missing transaction");
    }
}