// Application/Queries/Author/GetAllAuthorsQuery.cs
using MediatR;
using Application.DTOs;
using System.Collections.Generic;

namespace ApplicationLayers.Queries.Author
{
    public class GetAllAuthorsQuery : IRequest<List<AuthorDto>>
    {
    }
}
