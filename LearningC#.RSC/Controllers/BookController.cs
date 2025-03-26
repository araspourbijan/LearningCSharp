using LearningCSharp.RSC.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Commons;
using Shared.Dtos;
using Shared.Models;

namespace LearningCSharp.RSC.Controllers;
[ApiController]
[Route("books")]
public class BookController(BookService _service) : ControllerBase
{
    [HttpPost]
    public async Task<Result<Book>> CreateBookAsync(string title, string author, double price, int stock)
    {
        return await _service.CreateAsync(title, author, price, stock);
    }

    [HttpGet("list")]
    public async Task<IResult> GetBooksAsync()
    {
        return await _service.GetAllAsync();
    }

    [HttpGet("list/{id:Guid}")]
    public async Task<IResult> GetBookByIdAsync(Guid id)
    {
        return await _service.GetAByIdAsync(id);
    }
}
