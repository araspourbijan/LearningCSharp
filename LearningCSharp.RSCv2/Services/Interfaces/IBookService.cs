using Shared.Dtos;
using Shared.Models;

namespace LearningCSharp.RSCv2.Services.Interfaces;

public interface IBookService
{
    public Task CreateAsync(string title, string author, double price, int stock);
    public Task<List<BookDto>> GetAllAsync();
    public Task<BookDto> GetByIdAsync(Guid id);
    public Task UpdateAsync(Guid id, Book book);
    public Task DeleteAsync(Guid id);
}
