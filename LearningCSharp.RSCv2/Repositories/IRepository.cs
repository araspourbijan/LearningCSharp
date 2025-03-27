namespace LearningCSharp.RSCv2.Repositories;

public interface IRepository<T> where T : class
{
    Task CreateAsync(T obj);
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task UpdateAsync(Guid id, T obj);
    Task DeleteAsync(Guid id);
}
