using Application.DTOs;

using Application.Queries.Author;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLayers.Commands.Author;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAllAuthors()
        {
            var query = new GetAllAuthorsQuery();
            var authors = await _mediator.Send(query);
            return Ok(authors);
        }

        // GET: api/Author/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthorById(Guid id)
        {
            var query = new GetAuthorByIdQuery { Id = id };
            var author = await _mediator.Send(query);
            if (author == null)
                return NotFound();
            return Ok(author);
        }

        // POST: api/Author
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAuthor([FromBody] CreateAuthorCommand command)
        {
            var authorId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAuthorById), new { id = authorId }, authorId);
        }

        // PUT: api/Author/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] UpdateAuthorCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/Author/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var command = new DeleteAuthorCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
