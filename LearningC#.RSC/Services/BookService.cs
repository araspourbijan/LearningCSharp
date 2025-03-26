using AutoMapper;
using LearningCSharp.RSC.Repositories;
using Shared.Commons;
using Shared.Dtos;
using Shared.Models;

namespace LearningCSharp.RSC.Services;

public class BookService(IRepository<Book> _repository, IMapper _mapper)
{
    public async Task<Result<Book>> CreateAsync(string title, string author, double price, int stock)
    {
        //validation
        if (title.Length>100)
            return Result<Book>.Failure(new ErrorRecord(400, "Title is too long"));

        Book newBook = new() { Author = author, Title = title, Price = price, Stock = stock };

        return await _repository.CreateAsync(newBook);
    }

    public async Task<IResult> GetAllAsync()
    {
        var result = await _repository.GetAllAsync();
        if(!result.IsSuccess)
            return Results.Ok(result);

        var booksDto = _mapper.Map<List<BookDto>>(result.Data);

        var resultDto = Result<List<BookDto>>.FromResult(booksDto);

        return Results.Ok(resultDto);
    }

    public async Task<IResult> GetAByIdAsync(Guid id)
    {
        var result = await _repository.GetByIdAsync(id);
        if (!result.IsSuccess)
            return Results.Ok(result);

        BookDto bookDto = new()
        {
            Id = result.Data.Id,
            Title = result.Data.Title,
            Author = result.Data.Author,
            Price = result.Data.Price + " $",
            Stock = result.Data.Stock
        };
        var resultDto = Result<BookDto>.FromResult(bookDto);

        return Results.Ok(resultDto);

    }
}
