using LearningCSharp.RSCv2.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Models;

namespace LearningCSharp.RSCv2.Controllers;
[ApiController]
[Route("books")]
public class BookController(BookService _service) : ControllerBase
{
    [HttpPost]
    public async Task CreateBookAsync(string title, string author, double price, int stock)
    {
        await _service.CreateAsync(title, author, price, stock);
    }

    [HttpGet("list")]
    public async Task<List<BookDto>> GetBooksAsync()
    {
        return await _service.GetAllAsync();
    }

    [HttpGet("{id:Guid}")]
    public async Task GetBookByIdAsync(Guid id)
    {
        await _service.GetAByIdAsync(id);
    }

    [HttpPut("{id:Guid}")]
    public async Task UpdateAsync(Guid id, Book book)
    {
        await _service.UpdateAsync(id, book);
    }

    [HttpDelete("{id:Guid}")]
    public async Task DeleteAsync(Guid id)
    {
        await _service.DeleteAsync(id);
    }
}
