using Application.DTOs;
using Application.Exceptions;
using Application.Handlers.Book;
using Application.Interfaces;
using Domain.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class GetBookByIdQueryHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly GetBookByIdQueryHandler _handler;

    public GetBookByIdQueryHandlerTests()
    {
        // Initialize the mock repository
        _bookRepositoryMock = new Mock<IBookRepository>();
        _handler = new GetBookByIdQueryHandler(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnBookDto_WhenBookIsFound()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var authorId = Guid.NewGuid();
        var book = new Book
        {
            Id = bookId,
            Title = "Test Book",
            AuthorId = authorId,
            Author = new Author { Name = "Test Author" }
        };

        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(bookId))
            .ReturnsAsync(book);

        var query = new GetBookByIdQuery(bookId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(bookId, result.Id);
        Assert.Equal("Test Book", result.Title);
        Assert.Equal(authorId, result.AuthorId);
        Assert.Equal("Test Author", result.AuthorName);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenBookIsNotFound()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        _bookRepositoryMock.Setup(repo => repo.GetByIdAsync(bookId))
            .ReturnsAsync((Book)null); // Return null to simulate not found

        var query = new GetBookByIdQuery(bookId);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
    }
}
