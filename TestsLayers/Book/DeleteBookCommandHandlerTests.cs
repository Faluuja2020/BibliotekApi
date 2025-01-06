using Application.Interfaces;
using Application.Handlers.Book;
using ApplicationLayers.Commands.Book;
using Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class DeleteBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly DeleteBookCommandHandler _handler;

    public DeleteBookCommandHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _handler = new DeleteBookCommandHandler(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldDeleteBook_WhenBookExists()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var book = new Book { Id = bookId, Title = "Test Book", AuthorId = Guid.NewGuid() };

        _bookRepositoryMock
            .Setup(repo => repo.GetByIdAsync(bookId))
            .ReturnsAsync(book);

        _bookRepositoryMock
            .Setup(repo => repo.DeleteAsync(It.IsAny<Book>()))
            .Returns(Task.CompletedTask);

        var command = new DeleteBookCommand(bookId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(bookId, result);
        _bookRepositoryMock.Verify(repo => repo.GetByIdAsync(bookId), Times.Once);
        _bookRepositoryMock.Verify(repo => repo.DeleteAsync(It.Is<Book>(b => b.Id == bookId)), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenBookDoesNotExist()
    {
        // Arrange
        var bookId = Guid.NewGuid();

        _bookRepositoryMock
            .Setup(repo => repo.GetByIdAsync(bookId))
            .ReturnsAsync((Book?)null); // Simulate book not found

        var command = new DeleteBookCommand(bookId);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal("Book not found", exception.Message);

        _bookRepositoryMock.Verify(repo => repo.GetByIdAsync(bookId), Times.Once);
        _bookRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Book>()), Times.Never);
    }
}
