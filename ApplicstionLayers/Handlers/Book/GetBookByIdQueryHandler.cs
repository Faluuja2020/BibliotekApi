using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Book
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id);
            if (book == null)
                throw new NotFoundException(nameof(Book), request.Id);

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                AuthorId = book.AuthorId,
                AuthorName = book.Author?.Name
            };
        }
    }
}
