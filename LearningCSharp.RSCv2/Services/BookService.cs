using AutoMapper;
using LearningCSharp.RSCv2.Repositories;
using LearningCSharp.RSCv2.Services.Interfaces;
using Shared.Dtos;
using Shared.Exceptions;
using Shared.Models;

namespace LearningCSharp.RSCv2.Services;

public class BookService
{
    private readonly IRepository<Book> repository;
    private readonly IMapper mapper;

    public BookService(IRepository<Book> _repository, IMapper _mapper, INotificationService _notificationService, IDeliveryService _deliveryService)
    {
        repository = _repository;
        mapper = _mapper;
        BookCreated += _notificationService.OnObjectCreated; // ???
        BookCreatedDelivery += _deliveryService.OnObjectCreated;
        BookUpdated += _notificationService.OnObjectUpdated;

    }

    //public delegate void BookCreatedEventHandler(object source, EventArgs args);
    public delegate void BookCreatedEventHandler(object source, EmailMessageArgs args);

    public event BookCreatedEventHandler BookCreated; // first approach with EventArgs and delegate type
    public event EventHandler<DeliveryMessageArgs> BookCreatedDelivery; // second approach with generic EventHandler
    public event EventHandler<EmailMessageArgs> BookUpdated; // second approach with generic EventHandler
    public event EventHandler BookDeleted; // third approach with normal EventHandler
    protected virtual void OnBookCreated(EmailMessageArgs args)
    {
        BookCreated?.Invoke(this, args);
    }
    protected virtual void OnBookCreatedDelivery(DeliveryMessageArgs args)
    {
        BookCreatedDelivery?.Invoke(this, args);
    }

    protected virtual void OnBookUpdated(EmailMessageArgs args)
    {
        BookUpdated?.Invoke(this, args);
    }

    protected virtual void OnBookDeleted()
    {
        BookDeleted?.Invoke(this, EventArgs.Empty);
    }

    public async Task CreateAsync(string title, string author, double price, int stock)
    {
        //validation
        if (title.Length > 100)
            throw new BadRequestException("Title is too long");

        if (stock < 5)
            throw new ArgumentException("Stock must be at least 5", nameof(stock));

        Book newBook = new() { Author = author, Title = title, Price = price, Stock = stock };

        await repository.CreateAsync(newBook);

        EmailMessageArgs message = new()
        {
            Id = newBook.Id,
            Type = "CREATED",
            Email = "email@example.it",
            Subject = "new book has been created",
        };
        OnBookCreated(message);
        OnBookCreatedDelivery(new (){Id=newBook.Id, Title=newBook.Title, Type= newBook.Title, Stock= newBook.Stock});
    }

    public async Task<List<BookDto>> GetAllAsync()
    {
        var result = await repository.GetAllAsync();

        return mapper.Map<List<BookDto>>(result);
    }

    public async Task<BookDto> GetAByIdAsync(Guid id)
    {
        var result = await repository.GetByIdAsync(id);

        return mapper.Map<BookDto>(result);
    }

    public async Task UpdateAsync(Guid id, Book book)
    {
        await repository.UpdateAsync(id, book);
        OnBookUpdated(new()
        {
            Id = id,
            Type = "UPDATED",
            Email = "email@example.it",
            Subject = "this book has been created",
        });
    }

    public async Task DeleteAsync(Guid id)
    {
        await repository.DeleteAsync(id);
        OnBookDeleted();
    }
}
