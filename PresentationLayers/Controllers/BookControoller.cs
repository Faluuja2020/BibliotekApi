using Application.DTOs;
using ApplicationLayers.Commands.Book;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BookController> _logger;

        public BookController(IMediator mediator, ILogger<BookController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetAllBooks()
        {
            try
            {
                var query = new GetAllBooksQuery();
                var books = await _mediator.Send(query);
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching books");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid Guid format.");
            }

            try
            {
                var query = new GetBookByIdQuery(id);
                var book = await _mediator.Send(query);
                if (book == null)
                    return NotFound();

                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching book with ID: {id}");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBook([FromBody] CreateBookCommand command)
        {
            try
            {
                var bookId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetBookById), new { id = bookId }, bookId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating book");
                return BadRequest("Error creating book");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookCommand command)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid Guid format.");

            if (id != command.Id)
                return BadRequest("ID in URL doesn't match the ID in the request body.");

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating book with ID: {id}");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            try
            {
                var command = new DeleteBookCommand(id);
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting book with ID: {id}");
                return StatusCode(500, "An internal server error occurred.");
            }
        }
    }
}
