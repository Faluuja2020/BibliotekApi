using Application.DTOs;
using MediatR;

public class GetBookByIdQuery : IRequest<BookDto>
{
    public Guid Id { get; }

    public GetBookByIdQuery(Guid id)
    {
        Id = id;
    }
}
