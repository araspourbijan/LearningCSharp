using LearningCSharp.CQRS.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Enums;
using Shared.Exceptions;

namespace LearningCSharp.CQRS.Application.Books.Commands;

public record DeleteBookCommand(Guid Id) : IRequest;
public class DeleteBookHandler(IApplicationDbContext _context, IDistributedCache _distributedCache) : IRequestHandler<DeleteBookCommand>
{
    public async Task Handle(DeleteBookCommand request, CancellationToken ct)
    {
        if (!await _context.Books.AnyAsync(b => b.Id == request.Id, ct))
            throw new NotFoundException(request.Id);

        await _context.Books.Where(b => b.Id == request.Id).ExecuteDeleteAsync(ct);
        await _distributedCache.RemoveAsync(CacheEnums.BookList.ToString(), ct);
    }
}
