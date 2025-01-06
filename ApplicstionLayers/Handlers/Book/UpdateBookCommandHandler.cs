// Application/Handlers/Book/UpdateBookCommandHandler.cs

using Application.Interfaces;
using ApplicationLayers.Commands.Book;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Book
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Guid>
    {
        private readonly IBookRepository _bookRepository;

        public UpdateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Guid> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id);

            if (book == null)
            {
                //return an error or throw an exception)
                throw new Exception("Book not found");
            }

            // Update 
            book.Title = request.Title;
            book.AuthorId = request.AuthorId;

            // Save changes
            await _bookRepository.UpdateAsync(book);

            // updated book's ID
            return book.Id;
        }
    }
}
