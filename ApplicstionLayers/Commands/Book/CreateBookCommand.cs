using MediatR;

namespace ApplicationLayers.Commands.Book
{
    public class CreateBookCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
    }
}
