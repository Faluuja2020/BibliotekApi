using Application.DTOs;
using Application.Interfaces;
using MediatR;

public class GetAllBooksQuery : IRequest<List<BookDto>> { }

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<BookDto>>
{
    private readonly IBookRepository _bookRepository;

    public GetAllBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllAsync();

        var bookDtos = books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            AuthorId = b.AuthorId,
            AuthorName = b.Author != null ? b.Author.Name : "Unknown"
        }).ToList();

        return bookDtos;
    }
}
