using LearningCSharp.RSCv2.Exceptions;
using LearningCSharp.RSCv2.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LearningCSharp.RSCv2.Repositories;

public class GenericRepository<T>(ApplicationDbV2Context _context) : IRepository<T> where T : class
{
    public async Task CreateAsync(T obj)
    {
        _context.Set<T>().Add(obj);
        var result = await _context.SaveChangesAsync();

        if (result != 1)
            throw new BadRequestException();

        Results.Created();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        var result = await _context.Set<T>().FindAsync(id) ?? throw new NotFoundException(id);

        return result;
    }

    public async Task DeleteAsync(Guid id)
    {
        var item = await _context.Set<T>().FindAsync(id) ?? throw new NotFoundException(id);

        _context.Set<T>().Remove(item);
        var result = await _context.SaveChangesAsync();

        if (result != 1)
            throw new BadRequestException();

        Results.Ok();
    }

    public async Task UpdateAsync(Guid id, T obj) // poteva essere meglio?
    {
        var item = await _context.Set<T>().FindAsync(id);

        var res = _context.Set<T>().Update(obj) ?? throw new BadRequestException();

        var result = await _context.SaveChangesAsync();

        if (result != 1)
            throw new BadRequestException();

        Results.Ok();
    }
}
