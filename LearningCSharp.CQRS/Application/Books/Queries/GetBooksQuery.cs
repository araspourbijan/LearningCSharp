using LearningCSharp.CQRS.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace LearningCSharp.CQRS.Application.Books.Queries;

public record GetBooksQuery : IRequest<List<BookDto>>;
public class DeleteBookHandler(IApplicationDbContext _context) : IRequestHandler<GetBooksQuery, List<BookDto>>
{
    public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken ct)
    {
        var result = await _context.Books
            .AsNoTracking()
            .Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Price = b.Price.ToString("C"),
                Stock = b.Stock
            })
            .ToListAsync(ct);

        return result;
    }
}

