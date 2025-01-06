// ApplicationLayers/Commands/Author/DeleteAuthorCommand.cs
using MediatR;
using System;

namespace ApplicationLayers.Commands.Author
{
    public class DeleteAuthorCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public DeleteAuthorCommand(Guid id)
        {
            Id = id;
        }
    }
}
