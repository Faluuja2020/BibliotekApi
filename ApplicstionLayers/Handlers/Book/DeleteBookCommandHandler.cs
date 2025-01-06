// Application/Handlers/Book/DeleteBookCommandHandler.cs

using Application.Interfaces;
using ApplicationLayers.Commands.Book;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Book
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Guid>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Guid> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id);

            if (book == null)
            {
                // Handle book not found (e.g., return an error or throw an exception)
                throw new Exception("Book not found");
            }

            // Delete the book
            await _bookRepository.DeleteAsync(book);

            // Return the deleted book's ID
            return book.Id;
        }
    }
}
