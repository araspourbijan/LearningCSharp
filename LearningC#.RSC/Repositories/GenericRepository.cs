using LearningCSharp.RSC.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared.Commons;

namespace LearningCSharp.RSC.Repositories;

public class GenericRepository<T>(ApplicationDbContext _context) : IRepository<T> where T : class
{
    public async Task<Result<T>> CreateAsync(T obj)
    {
        if (obj == null)
            return Result<T>.Failure(Errors.NullArgument);

        _context.Set<T>().Add(obj);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Result<T>.Created() : Result<T>.Failure(Errors.BadRequest);
    }

    public async Task<Result<T>> DeleteAsync(Guid id)
    {
        var item = await _context.Set<T>().FindAsync(id);

        if (item == null)
            return Result<T>.Failure(Errors.BadRequest);

        _context.Set<T>().Remove(item);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? Result<T>.Success() : Result<T>.Failure(Errors.ServiceUnavailable);
    }

    public async Task<Result<List<T>>> GetAllAsync()
    {
        var result = await _context.Set<T>().ToListAsync();

        return Result<List<T>>.FromResult(result);
    }

    public async Task<Result<T>> GetByIdAsync(Guid id)
    {
        var result = await _context.Set<T>().FindAsync(id);
        return result == null ? Result<T>.Failure(Errors.NotFound(id)) : Result<T>.FromResult(result);
    }

    public async Task<Result<T>> UpdateAsync(Guid id, T obj)
    {
        var item = await _context.Set<T>().FindAsync(id);
        if (item == null)
            return Result<T>.Failure(Errors.NotFound(id));

        var res = _context.Set<T>().Update(obj);
        if (res == null)
            return Result<T>.Failure(Errors.BadRequest);

        var result = await _context.SaveChangesAsync();

        return result > 0 ? Result<T>.Success() : Result<T>.Failure(Errors.BadRequest);
    }
}
