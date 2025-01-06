// ApplicationLayers/Commands/Author/CreateAuthorCommand.cs
using MediatR;
using System;

namespace ApplicationLayers.Commands.Author
{
    public class CreateAuthorCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public CreateAuthorCommand(string name)
        {
            Name = name;
        }
    }
}
