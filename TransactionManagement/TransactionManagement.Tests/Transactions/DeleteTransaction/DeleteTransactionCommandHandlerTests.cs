using Moq;
using FluentAssertions;
using TransactionManagement.Core.Transactions;
using TransactionManagement.Core.Transactions.DeleteTransaction;

namespace TransactionManagement.Tests.DeleteTransaction;

/// <summary>
/// Unit tests for the DeleteTransactionCommandHandler.
/// These tests verify behavior when deleting financial transactions,
/// ensuring correct handling of both successful and failed deletion scenarios.
/// </summary>
public class DeleteTransactionCommandHandlerTests
{
    /// <summary>
    /// Test: Deleting a transaction that exists should succeed.
    /// Verifies that:
    /// - The handler retrieves the transaction by ID
    /// - The repository's DeleteAsync is called exactly once
    /// - A success Result is returned
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldReturnSuccess_WhenTransactionExists()
    {
        // Arrange: Mock repository to return a valid transaction for the given ID
        var id = Guid.NewGuid();
        var existingTransaction = new Transaction(id, DateTime.Now, "Sample", 10m);

        var repoMock = new Mock<ITransactionRepository>();
        repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(existingTransaction);
        repoMock.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask);

        var handler = new DeleteTransactionCommandHandler(repoMock.Object);
        var command = new DeleteTransactionCommand { Id = id };

        // Act: Execute the handler
        var result = await handler.HandleAsync(command);

        // Assert: Ensure success and verify DeleteAsync was called once
        result.IsSuccess.Should().BeTrue("the transaction exists and should be deleted successfully");
        repoMock.Verify(r => r.DeleteAsync(id), Times.Once, "DeleteAsync must be called once when transaction exists");
    }

    /// <summary>
    /// Test: Deleting a transaction that does not exist should fail.
    /// Verifies that:
    /// - The handler checks for existence
    /// - Returns a failure Result with a meaningful error message
    /// - DeleteAsync is not called
    /// </summary>
    [Fact]
    public async Task HandleAsync_ShouldReturnFailure_WhenTransactionDoesNotExist()
    {
        // Arrange: Mock repository to return null (transaction not found)
        var id = Guid.NewGuid();

        var repoMock = new Mock<ITransactionRepository>();
        repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Transaction?)null);

        var handler = new DeleteTransactionCommandHandler(repoMock.Object);
        var command = new DeleteTransactionCommand { Id = id };

        // Act: Attempt to delete a non-existing transaction
        var result = await handler.HandleAsync(command);

        // Assert: Ensure failure and validate the error message
        result.IsFailure.Should().BeTrue("the transaction does not exist and cannot be deleted");
        result.Error.Should().Contain("not found", "the error message should clearly indicate the issue");
        repoMock.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Never, "DeleteAsync must not be called for non-existent transaction");
    }
}