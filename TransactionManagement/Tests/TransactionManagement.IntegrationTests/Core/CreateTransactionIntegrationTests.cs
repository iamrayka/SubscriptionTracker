using FluentAssertions;
using TransactionManagement.Core.Transactions.CreateTransaction;
using TransactionManagement.Infrastructure.Transactions;

namespace TransactionManagement.IntegrationTests.Core.CreateTransaction;

/// <summary>
/// Integration tests for CreateTransactionCommandHandler using the real in-memory repository.
/// These tests verify the end-to-end behavior of the handler and the repository without mocks.
/// </summary>
public class CreateTransactionIntegrationTests
{
    /// <summary>
    /// Test: A valid CreateTransactionCommand should persist the transaction in the in-memory repository.
    /// This integration test verifies that:
    /// - The CreateTransactionCommandHandler works correctly with the real TransactionRepository
    /// - The created transaction is persisted and retrievable
    /// - The persisted data matches the input command
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldPersistTransaction_WhenCommandIsValid()
    {
        // Arrange: Set up a real in-memory repository and the command handler
        var repo = new TransactionRepository(); // Using real (not mocked) repository
        var handler = new CreateTransactionCommandHandler(repo); // Handler depends on the repository

        // Prepare a valid transaction creation command
        var command = new CreateTransactionCommand
        {
            UserId = Guid.NewGuid(), // Unique user ID
            TransactionDate = DateTime.Today,
            Description = "Integration Test",
            Amount = 200m
        };

        // Act: Execute the handler with the valid command
        var result = await handler.HandleAsync(command);

        // Assert: Ensure the result is successful and contains a non-null transaction
        result.IsSuccess.Should().BeTrue("a valid command should succeed");
        result.Value.Should().NotBeNull("the result should contain the created transaction");

        // Retrieve the saved transaction directly from the repository
        var saved = await repo.GetByIdAsync(result.Value.Id);

        // Verify that the saved transaction matches the command input
        saved.Should().NotBeNull("the transaction should have been persisted");
        saved!.Description.Should().Be("Integration Test", "the description should match");
        saved.Amount.Should().Be(200m, "the amount should match");
    }

    /// <summary>
    /// Test: Creating a transaction with an empty description should fail.
    /// This validates that the domain validation logic is enforced at the integration level.
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldReturnFailure_WhenDescriptionIsEmpty()
    {
        // Arrange: Use the real repository and handler as usual
        var repo = new TransactionRepository();
        var handler = new CreateTransactionCommandHandler(repo);

        // Prepare an invalid command with an empty description
        var command = new CreateTransactionCommand
        {
            UserId = Guid.NewGuid(),
            TransactionDate = DateTime.Today,
            Description = "", // Invalid input
            Amount = 100
        };

        // Act: Attempt to create the transaction
        var result = await handler.HandleAsync(command);

        // Assert: Ensure the result is a failure and contains the appropriate error message
        result.IsFailure.Should().BeTrue("a transaction with an empty description should be rejected");
        result.Error.Should().NotBeNullOrWhiteSpace("an error message should be returned");
        result.Error.Should().Be("Invalid input provided.", "validation errors should return a generic user-safe message");
    }
}
