// Application/Handlers/Author/GetAllAuthorsQueryHandler.cs
using Application.DTOs;
using Application.Interfaces;
using Application.Queries.Author;
using ApplicstionLayers.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Author
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDto>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<AuthorDto> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetByIdAsync(request.Id);
            if (author == null)
                return null;  // Or handle "not found" scenario

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name
            };
        }
    }
}

