// ApplicationLayers/Commands/Author/UpdateAuthorCommand.cs
using MediatR;
using System;

namespace ApplicationLayers.Commands.Author
{
    public class UpdateAuthorCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public UpdateAuthorCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
