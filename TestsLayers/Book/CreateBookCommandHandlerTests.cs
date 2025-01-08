using Application.Interfaces;
using ApplicationLayers.Commands.Book;
using ApplicationLayers.Handlers.Book;
using Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


public class CreateBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly CreateBookCommandHandler _handler;

    public CreateBookCommandHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _handler = new CreateBookCommandHandler(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnBookId_WhenBookIsCreatedSuccessfully()
    {
        // Arrange
        var command = new CreateBookCommand
        {
            Title = "Test Book",
            AuthorId = Guid.NewGuid()
        };

        var bookId = Guid.NewGuid();
        _bookRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Book>()))
            .Callback<Book>(book => book.Id = bookId) // Simulate setting the ID
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(bookId, result);
        _bookRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Book>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowArgumentNullException_WhenCommandIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null!, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldCallRepositoryAddAsync_WithCorrectBook()
    {
        // Arrange
        var command = new CreateBookCommand
        {
            Title = "Test Book",
            AuthorId = Guid.NewGuid()
        };

        Book? capturedBook = null;
        _bookRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Book>()))
            .Callback<Book>(book => capturedBook = book)
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(capturedBook);
        Assert.Equal(command.Title, capturedBook.Title);
        Assert.Equal(command.AuthorId, capturedBook.AuthorId);
        _bookRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Book>()), Times.Once);
    }
}
