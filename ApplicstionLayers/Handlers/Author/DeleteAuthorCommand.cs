// Application/Handlers/Author/DeleteAuthorCommandHandler.cs
using Application.Interfaces;
using ApplicationLayers.Commands.Author;
using ApplicstionLayers.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Author
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Guid>
    {
        private readonly IAuthorRepository _authorRepository;

        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Guid> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetByIdAsync(request.Id);
            if (author == null)
            {
                // Handle not found scenario, could throw an exception or return a specific value
                return Guid.Empty;
            }

            await _authorRepository.DeleteAsync(author);
            return author.Id;
        }
    }
}
