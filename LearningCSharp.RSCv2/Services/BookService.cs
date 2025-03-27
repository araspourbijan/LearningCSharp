using AutoMapper;
using LearningCSharp.RSCv2.Exceptions;
using LearningCSharp.RSCv2.Repositories;
using Shared.Dtos;
using Shared.Models;

namespace LearningCSharp.RSCv2.Services;

public class BookService(IRepository<Book> _repository, IMapper _mapper)
{
    public async Task CreateAsync(string title, string author, double price, int stock)
    {
        //validation
        if (title.Length > 100)
            throw new BadRequestException("Title is too long");

        Book newBook = new() { Author = author, Title = title, Price = price, Stock = stock };

        await _repository.CreateAsync(newBook);
    }

    public async Task<List<BookDto>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync();

        return _mapper.Map<List<BookDto>>(result);
    }

    public async Task<BookDto> GetAByIdAsync(Guid id)
    {
        var result = await _repository.GetByIdAsync(id);

        return _mapper.Map<BookDto>(result);
    }

    public async Task UpdateAsync(Guid id, Book book)
    {
        await _repository.UpdateAsync(id, book);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
