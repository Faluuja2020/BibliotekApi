using FluentValidation;
using Domain.Entities;

using ApplicationLayers.Commands.Book;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.AuthorId).NotEmpty().WithMessage("Author is required.");
    }
}
