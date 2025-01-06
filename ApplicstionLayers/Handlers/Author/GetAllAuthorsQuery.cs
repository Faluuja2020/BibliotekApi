// Application/Queries/Author/GetAllAuthorsQuery.cs
using Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Author
{
    public class GetAllAuthorsQuery : IRequest<List<AuthorDto>>
    {
    }
}
