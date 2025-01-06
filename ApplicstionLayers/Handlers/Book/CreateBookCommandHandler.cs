using Application.Interfaces;
using ApplicationLayers.Commands.Book;
using Domain.Entities;
using MediatR;

namespace ApplicationLayers.Handlers.Book
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
    {
        private readonly IBookRepository _bookRepository;

        public CreateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "The command cannot be null.");
            }

            var book = new Domain.Entities.Book
            {
                Title = request.Title,
                AuthorId = request.AuthorId
            };

            await _bookRepository.AddAsync(book);
            return book.Id;
        }

    }
}
