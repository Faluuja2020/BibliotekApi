// Application/Handlers/Author/CreateAuthorCommandHandler.cs
using Application.Interfaces;
using ApplicationLayers.Commands.Author;
using ApplicstionLayers.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Author
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Guid>
    {
        private readonly IAuthorRepository _authorRepository;

        public CreateAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Domain.Entities.Author
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            await _authorRepository.AddAsync(author);
            return author.Id;
        }
    }
}
