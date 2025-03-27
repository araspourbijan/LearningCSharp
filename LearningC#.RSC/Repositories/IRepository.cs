using Shared.Commons;
using Shared.Models;

namespace LearningCSharp.RSC.Repositories;

public interface IRepository<T> where T : class
{
    Task<Result<T>> CreateAsync(T obj);
    Task<Result<List<T>>> GetAllAsync();
    Task<Result<T>> GetByIdAsync(Guid id);
    Task<Result<T>> UpdateAsync(Guid id, T obj);
    Task<Result<T>> DeleteAsync(Guid id);
}
