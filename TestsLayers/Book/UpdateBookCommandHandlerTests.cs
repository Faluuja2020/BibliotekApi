using Application.Interfaces;
using Application.Handlers.Book;
using ApplicationLayers.Commands.Book;
using Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class UpdateBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly UpdateBookCommandHandler _handler;

    public UpdateBookCommandHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _handler = new UpdateBookCommandHandler(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldUpdateBook_WhenBookExists()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var existingBook = new Book { Id = bookId, Title = "Old Title", AuthorId = Guid.NewGuid() };

        _bookRepositoryMock
            .Setup(repo => repo.GetByIdAsync(bookId))
            .ReturnsAsync(existingBook);

        _bookRepositoryMock
            .Setup(repo => repo.UpdateAsync(It.IsAny<Book>()))
            .Returns(Task.CompletedTask);

        var command = new UpdateBookCommand(bookId, "Updated Title", Guid.NewGuid());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(bookId, result);
        Assert.Equal(command.Title, existingBook.Title);
        Assert.Equal(command.AuthorId, existingBook.AuthorId);
        _bookRepositoryMock.Verify(repo => repo.GetByIdAsync(bookId), Times.Once);
        _bookRepositoryMock.Verify(repo => repo.UpdateAsync(existingBook), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenBookDoesNotExist()
    {
        // Arrange
        var bookId = Guid.NewGuid();

        _bookRepositoryMock
            .Setup(repo => repo.GetByIdAsync(bookId))
            .ReturnsAsync((Book?)null); // Simulate book not found

        var command = new UpdateBookCommand(bookId, "Updated Title", Guid.NewGuid());

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal("Book not found", exception.Message);

        _bookRepositoryMock.Verify(repo => repo.GetByIdAsync(bookId), Times.Once);
        _bookRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Book>()), Times.Never);
    }
}
