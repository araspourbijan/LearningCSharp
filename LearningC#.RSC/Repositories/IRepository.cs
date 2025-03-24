namespace LearningCSharp.RSC.Repositories;

public interface IRepository<T> where T : class
{
    Task<IResult> CreateAsync(T obj);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IResult> GetByIdAsync(Guid id);
    Task<IResult> UpdateAsync(T obj);
    Task<IResult> DeleteAsync(Guid id);
}
