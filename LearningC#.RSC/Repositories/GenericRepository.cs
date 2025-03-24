
using LearningCSharp.RSC.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LearningCSharp.RSC.Repositories;

public class GenericRepository<T>(ApplicationDbContext _context) : IRepository<T> where T : class
{
    public async Task<IResult> CreateAsync(T obj)
    {
        if (obj == null)
            return Results.BadRequest();

        _context.Set<T>().Add(obj);
        await _context.SaveChangesAsync();

        return Results.Created();
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var item = await _context.Set<T>().FindAsync(id);

        if (item == null)
            return Results.NotFound();

        _context.Set<T>().Remove(item);
        await _context.SaveChangesAsync();

        return Results.Ok();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IResult> GetByIdAsync(Guid id)
    {
        var item = await _context.Set<T>().FindAsync(id);
        return item != null ? Results.Ok(item) : Results.NotFound();
    }

    public async Task<IResult> UpdateAsync(T obj)
    {
        if (obj == null)
            Results.NotFound();

        _context.Set<T>().Update(obj);
        await _context.SaveChangesAsync();

        return Results.Ok();
    }
}
