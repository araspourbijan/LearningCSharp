using LearningCSharp.CQRS.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Shared.Dtos;

namespace LearningCSharp.CQRS.Application.Books.Queries;

public record GetBooksQuery : IRequest<List<BookDto>>;
public class GetBooksQueryHandler(IApplicationDbContext _context, ILogger<GetBooksQueryHandler> _logger, IMemoryCache _memoryCache/*, IDistributedCache _distributedCache*/) : IRequestHandler<GetBooksQuery, List<BookDto>>
{
    public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken ct)
    {
        var cacheKey = $"books-{nameof(GetBooksQuery)}";
        if (!_memoryCache.TryGetValue(cacheKey, out List<BookDto>? result))
        {
            _logger.LogInformation("Cache miss for {CacheKey}", cacheKey);
            Thread.Sleep(2000);
            result = await _context.Books
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

            _memoryCache.Set(cacheKey, result, TimeSpan.FromMinutes(1));
            _logger.LogInformation("Cache set for {CacheKey}", cacheKey);
        }

        //var distributedResult = await _distributedCache.GetAsync(cacheKey, ct);

        //if (distributedResult == null)
        //{
        //    Thread.Sleep(4000);
        //    var dataToSerialize = await _context.Books
        //       .AsNoTracking()
        //       .Select(b => new BookDto
        //       {
        //           Id = b.Id,
        //           Title = b.Title,
        //           Author = b.Author,
        //           Price = b.Price.ToString("C"),
        //           Stock = b.Stock
        //       })
        //       .ToListAsync(ct);

        //    var serialized = JsonSerializer.Serialize(dataToSerialize, CacheSourceGenerationContext.Default.ListBookDto);

        //    await _distributedCache.SetAsync(cacheKey, JsonSerializer.SerializeToUtf8Bytes(serialized), new DistributedCacheEntryOptions
        //    {
        //        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        //    }, ct);

        //    return dataToSerialize;
        //}

        //var result = JsonSerializer.Deserialize<List<BookDto>>(Encoding.UTF8.GetString(distributedResult), CacheSourceGenerationContext.Default.ListBookDto);

        return result!;
    }
}

