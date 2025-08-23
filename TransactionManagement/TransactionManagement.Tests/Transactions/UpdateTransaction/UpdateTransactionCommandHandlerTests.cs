using Moq;
using FluentAssertions;
using TransactionManagement.Core.Transactions;
using TransactionManagement.Core.Transactions.UpdateTransaction;

namespace TransactionManagement.Tests.UpdateTransaction;

/// <summary>
/// Unit tests for UpdateTransactionCommandHandler.
/// These tests validate behavior for updating financial transactions,
/// including successful updates and expected failures.
/// </summary>
public class UpdateTransactionCommandHandlerTests
{
    /// <summary>
    /// Test: Valid transaction update should succeed.
    /// This test ensures that when a valid command is given, the handler:
    /// - Retrieves the transaction
    /// - Applies the update
    /// - Persists the changes via the repository
    /// - Returns a successful Result containing the updated transaction
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldUpdateTransaction_WhenTransactionExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var existingTransaction = new Transaction(id, DateTime.Today.AddDays(-1), "Old Description", 50);

        // Mocking repository behavior: return an existing transaction
        var repoMock = new Mock<ITransactionRepository>();
        repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(existingTransaction);
        repoMock.Setup(r => r.UpdateAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);

        var handler = new UpdateTransactionCommandHandler(repoMock.Object);

        // Creating a valid update command
        var command = new UpdateTransactionCommand
        {
            Id = id,
            TransactionDate = DateTime.Today,
            Description = "Updated Description",
            Amount = 99.99m
        };

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue("the update should succeed with valid data");
        result.Value.Description.Should().Be("Updated Description", "the description should be updated");
        result.Value.Amount.Should().Be(99.99m, "the amount should reflect the new value");
        repoMock.Verify(r => r.UpdateAsync(It.IsAny<Transaction>()), Times.Once, "the update method must be called once");
    }

    /// <summary>
    /// Test: Update command with empty ID should fail.
    /// This ensures that invalid input is caught early without hitting the repository.
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldReturnFailure_WhenTransactionIdIsEmpty()
    {
        // Arrange
        var handler = new UpdateTransactionCommandHandler(new Mock<ITransactionRepository>().Object);

        // Passing an invalid command (empty GUID)
        var command = new UpdateTransactionCommand
        {
            Id = Guid.Empty,
            TransactionDate = DateTime.Today,
            Description = "Some Description",
            Amount = 10
        };

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsFailure.Should().BeTrue("an empty transaction ID is invalid and should not be processed");
        result.Error.Should().Contain("Transaction ID", "the error message should point to the invalid ID");
    }

    /// <summary>
    /// Test: Update command should fail if the transaction does not exist.
    /// Verifies that the handler gracefully handles missing records and avoids null reference errors.
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldReturnFailure_WhenTransactionNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Mocking the repository to simulate a missing transaction
        var repoMock = new Mock<ITransactionRepository>();
        repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Transaction?)null);

        var handler = new UpdateTransactionCommandHandler(repoMock.Object);

        // Command for a transaction that doesn’t exist
        var command = new UpdateTransactionCommand
        {
            Id = id,
            TransactionDate = DateTime.Today,
            Description = "Doesn't Matter",
            Amount = 10
        };

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsFailure.Should().BeTrue("the update should fail if the transaction doesn't exist");
        result.Error.Should().Contain("not found", "the error message should clearly indicate the cause");
        repoMock.Verify(r => r.UpdateAsync(It.IsAny<Transaction>()), Times.Never, "update should not be attempted on null");
    }
}