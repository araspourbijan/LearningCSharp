using LearningCSharp.CQRS.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Shared.Dtos;
using Shared.Enums;
using System.Text;
using System.Text.Json;

namespace LearningCSharp.CQRS.Application.Books.Queries;

public record GetBooksQuery : IRequest<List<BookDto>>;
public class GetBooksQueryHandler(IApplicationDbContext _context, ILogger<GetBooksQueryHandler> _logger, IMemoryCache _memoryCache, IDistributedCache _distributedCache) : IRequestHandler<GetBooksQuery, List<BookDto>>
{
    public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken ct)
    {
        var cacheKey = CacheEnums.BookList.ToString(); // Cache key for the list of books

        // in memory cache

        //if (!_memoryCache.TryGetValue(cacheKey, out List<BookDto>? result))
        //{
        //    _logger.LogDebug("Cache miss for {CacheKey}", cacheKey);

        //    Thread.Sleep(1000);
        //    result = await _context.Books
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

        //    _memoryCache.Set(cacheKey, result, TimeSpan.FromSeconds(1));
        //    _logger.LogDebug("Cache set for {CacheKey} using IMemoryCache", cacheKey);
        //}

        // in memory cache


        var distributedResult = await _distributedCache.GetAsync(cacheKey, ct);
        JsonSerializerOptions _jsonSerializerOptions = new(defaults: JsonSerializerDefaults.Web);

        if (distributedResult == null)
        {
            _logger.LogDebug("Cache miss for {CacheKey}", cacheKey);

            Thread.Sleep(4000);
            var dataToSerialize = await _context.Books
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



            //var serialized = JsonSerializer.Serialize<List<BookDto>>(dataToSerialize, CacheSourceGenerationContext.Default.ListBookDto);

            var serialized = JsonSerializer.SerializeToUtf8Bytes(dataToSerialize, _jsonSerializerOptions);
            await _distributedCache.SetAsync(cacheKey, serialized, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(15)
            }, ct);

            _logger.LogDebug("Cache set for {CacheKey} using IDistributedCache", cacheKey);

            return dataToSerialize;
        }

        //var result = JsonSerializer.Deserialize(Encoding.UTF8.GetString(distributedResult), CacheSourceGenerationContext.Default.ListBookDto);
        var result = JsonSerializer.Deserialize<List<BookDto>>(Encoding.UTF8.GetString(distributedResult), _jsonSerializerOptions);

        return result!;
    }
}

