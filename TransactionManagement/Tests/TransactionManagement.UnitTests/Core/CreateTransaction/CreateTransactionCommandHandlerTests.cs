using Moq;
using FluentAssertions;
using TransactionManagement.Core.Transactions;
using TransactionManagement.Core.Transactions.CreateTransaction;

namespace TransactionManagement.Tests.CreateTransaction;

/// <summary>
/// Unit tests for the CreateTransactionCommandHandler.
/// Validates behavior for creating new financial transactions,
/// including successful creation and validation error handling.
/// </summary>
public class CreateTransactionCommandHandlerTests
{
    /// <summary>
    /// Test: A valid CreateTransactionCommand should result in a successful transaction creation.
    /// Verifies that:
    /// - The handler builds a valid Transaction entity
    /// - The repository's CreateAsync is invoked exactly once
    /// - The returned Result is a success with a non-null Transaction
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldCreateTransaction_WhenInputIsValid()
    {
        // Arrange: Create a mock repository and handler
        var repoMock = new Mock<ITransactionRepository>();
        var handler = new CreateTransactionCommandHandler(repoMock.Object);

        var command = new CreateTransactionCommand
        {
            UserId = Guid.NewGuid(),
            TransactionDate = DateTime.Today,
            Description = "Test Transaction",
            Amount = 99.99m
        };

        // Act: Execute the command through the handler
        var result = await handler.HandleAsync(command);

        // Assert: Verify the result is successful and the repository was called once
        result.IsSuccess.Should().BeTrue("the command is valid and should result in a successful transaction");
        result.Value.Should().NotBeNull("a successful result must contain a transaction object");
        result.Value.Description.Should().Be("Test Transaction", "the transaction description should match the input");
        result.Value.Amount.Should().Be(99.99m, "the transaction amount should match the input");
        repoMock.Verify(r => r.CreateAsync(It.IsAny<Transaction>()), Times.Once, "repository should be called exactly once");
    }

    /// <summary>
    /// Test: Handler should return a failed result when the Description is empty.
    /// This validates that the domain rules in Transaction constructor throw an exception,
    /// which is then caught and wrapped in a Result.Failure.
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldReturnFailure_WhenDescriptionIsEmpty()
    {
        // Arrange: Create a command with invalid description
        var repoMock = new Mock<ITransactionRepository>();
        var handler = new CreateTransactionCommandHandler(repoMock.Object);

        var command = new CreateTransactionCommand
        {
            UserId = Guid.NewGuid(),
            TransactionDate = DateTime.Today,
            Description = "", // Invalid: empty
            Amount = 50
        };

        // Act: Attempt to create the transaction
        var result = await handler.HandleAsync(command);

        // Assert: Verify failure and error message
        result.IsFailure.Should().BeTrue("empty description is invalid and should result in failure");
        result.Error.Should().NotBeNullOrWhiteSpace("an appropriate error message should be returned");
        result.Error.Should().Be("Invalid input provided.", "a generic failure message should be returned for invalid input");
    }

    /// <summary>
    /// Test: Handler should return a failure result when UserId is empty.
    /// This verifies input validation for required UserId.
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldReturnFailure_WhenUserIdIsEmpty()
    {
        // Arrange
        var repoMock = new Mock<ITransactionRepository>();
        var handler = new CreateTransactionCommandHandler(repoMock.Object);

        var command = new CreateTransactionCommand
        {
            UserId = Guid.Empty, // Invalid
            TransactionDate = DateTime.Today,
            Description = "Test",
            Amount = 100
        };

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        result.IsFailure.Should().BeTrue("an empty UserId is invalid and should fail");
        result.Error.Should().Contain("User ID", "the error should clearly indicate the invalid field");
        repoMock.Verify(r => r.CreateAsync(It.IsAny<Transaction>()), Times.Never, "the repository should not be called on invalid input");
    }
}