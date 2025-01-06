// ApplicationLayers/Commands/Book/UpdateBookCommand.cs
using MediatR;
using System;

namespace ApplicationLayers.Commands.Book
{
    public class UpdateBookCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid AuthorId { get; set; }

        public UpdateBookCommand(Guid id, string title, Guid authorId)
        {
            Id = id;
            Title = title;
            AuthorId = authorId;
        }
    }
}
