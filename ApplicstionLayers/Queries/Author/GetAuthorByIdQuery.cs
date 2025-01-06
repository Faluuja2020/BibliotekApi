// Application/Queries/Author/GetAuthorByIdQuery.cs
using MediatR;
using Application.DTOs;

namespace Application.Queries.Author
{
    public class GetAuthorByIdQuery : IRequest<AuthorDto>
    {
        public Guid Id { get; set; }
    }
}
