using LearningCSharp.RSCv2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Models;

namespace LearningCSharp.RSCv2.Controllers;

[ApiController]
[Route("books")]
public class BookController(IBookService _bookService) : ControllerBase
{
    [HttpPost]
    public async Task CreateBookAsync(string title, string author, double price, int stock)
    {
        await _bookService.CreateAsync(title, author, price, stock);
    }

    //[OutputCache]
    [HttpGet("list")]
    public async Task<List<BookDto>> GetBooksAsync()
    {
        return await _bookService.GetAllAsync();
    }

    [ResponseCache(Duration = 20, VaryByQueryKeys = ["id"])]
    [HttpGet("{id:Guid}")]
    public async Task<BookDto> GetBookByIdAsync(Guid id)
    {
        return await _bookService.GetByIdAsync(id);
    }

    [HttpPut("{id:Guid}")]
    public async Task UpdateAsync(Guid id, Book book)
    {
        await _bookService.UpdateAsync(id, book);
    }

    [HttpDelete("{id:Guid}")]
    public async Task DeleteAsync(Guid id)
    {
        await _bookService.DeleteAsync(id);
    }
}
