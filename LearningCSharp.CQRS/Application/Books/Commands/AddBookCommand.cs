using FluentValidation;
using LearningCSharp.CQRS.Application.Interfaces;
using MediatR;
using Shared.Models;

namespace LearningCSharp.CQRS.Application.Books.Commands;

public record AddBookCommand(string Title, string Author, double Price, int Stock) : IRequest;
public class AddBookHandler(IApplicationDbContext _context) : IRequestHandler<AddBookCommand>
{
    public async Task Handle(AddBookCommand request, CancellationToken ct)
    {
        var book = new Book
        {
            Title = request.Title.Trim(),
            Author = request.Author.Trim(),
            Price = request.Price,
            Stock = request.Stock,
            CreatedAt = DateTime.UtcNow
        };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        Results.Created(string.Empty, book.Id);
    }
}
public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
{
    public AddBookCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Author).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Stock).GreaterThan(5);
    }
}
