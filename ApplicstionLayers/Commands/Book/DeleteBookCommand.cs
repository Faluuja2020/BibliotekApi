// ApplicationLayers/Commands/Book/DeleteBookCommand.cs
using MediatR;
using System;

namespace ApplicationLayers.Commands.Book
{
    public class DeleteBookCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public DeleteBookCommand(Guid id)
        {
            Id = id;
        }
    }
}
