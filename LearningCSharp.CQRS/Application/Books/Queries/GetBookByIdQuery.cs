using LearningCSharp.CQRS.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;

namespace LearningCSharp.CQRS.Application.Books.Queries;

public record GetBookByIdQuery(Guid Id) : IRequest<BookDto>;
public class GetBookByIdHandler(IApplicationDbContext _context) : IRequestHandler<GetBookByIdQuery, BookDto>
{
    public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken ct)
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
            .FirstOrDefaultAsync(b => b.Id == request.Id) ?? throw new NotFoundException(request.Id);

        return result;
    }
}

