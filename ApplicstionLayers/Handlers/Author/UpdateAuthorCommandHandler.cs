// Application/Handlers/Author/UpdateAuthorCommandHandler.cs
using Application.Interfaces;
using ApplicationLayers.Commands.Author;
using ApplicstionLayers.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Author
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Guid>
    {
        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Guid> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetByIdAsync(request.Id);
            if (author == null)
            {
                // Handle not found scenario, could throw an exception or return a specific value
                return Guid.Empty;
            }

            author.Name = request.Name;
            await _authorRepository.UpdateAsync(author);
            return author.Id;
        }
    }
}
