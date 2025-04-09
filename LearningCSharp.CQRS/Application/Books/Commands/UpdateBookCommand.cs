using FluentValidation;
using LearningCSharp.CQRS.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Enums;
using Shared.Exceptions;
using Shared.Models;

namespace LearningCSharp.CQRS.Application.Books.Commands;

public record UpdateBookCommand(Guid Id, string Title, string Author, double? Price, int? Stock) : IRequest;
public class UpdateBookHandler(IApplicationDbContext _context, IDistributedCache _distributedCache) : IRequestHandler<UpdateBookCommand>
{
    public async Task Handle(UpdateBookCommand request, CancellationToken ct)
    {
        Book book = await _context.Books.FindAsync([request.Id], cancellationToken: ct) ?? throw new NotFoundException(request.Id);

        book.Title = request.Title.Trim();
        book.Author = request.Author.Trim();
        book.UpdatedAt = DateTime.UtcNow;

        if (request.Price.HasValue)
            book.Price = request.Price.Value;

        if (request.Stock.HasValue)
            book.Stock = request.Stock.Value;

        await _context.SaveChangesAsync();
        await _distributedCache.RemoveAsync(CacheEnums.BookList.ToString(), ct);

        Results.Ok();
    }
}
public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Author).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Price).GreaterThan(0).When(x => x.Price.HasValue);
        RuleFor(x => x.Stock).GreaterThan(0).When(x => x.Stock.HasValue);
    }
}
