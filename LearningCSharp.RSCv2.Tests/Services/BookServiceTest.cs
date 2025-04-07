using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using LearningCSharp.RSCv2.Infrastructure;
using LearningCSharp.RSCv2.Repositories;
using LearningCSharp.RSCv2.Services;
using LearningCSharp.RSCv2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;
using Shared.Models;

namespace LearningCSharp.RSCv2.Tests.Services;
public class BookServiceTest
{
    private readonly IBookService _bookService;
    private readonly ApplicationDbV2Context _context;
    private readonly IRepository<Book> _repository;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;
    private readonly IDeliveryService _deliveryService;
    public BookServiceTest()
    {
        _context = TestUtilities.GetDbContext();
        _repository = new GenericRepository<Book>(_context);
        _mapper = A.Fake<IMapper>();
        _notificationService = A.Fake<INotificationService>();
        _deliveryService = A.Fake<IDeliveryService>();
        _bookService = new BookService(_repository, _mapper, _notificationService, _deliveryService);
    }

    [Fact]
    public async Task CreateBook_ShouldAddBookToDatabase()
    {
        // Arrange
        var title = "Test Book";
        var author = "Test Author";
        var price = 19.99;
        var stock = 10;
        // Act
        await _bookService.CreateAsync(title, author, price, stock);
        // Assert
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Title == title);
        Assert.NotNull(book);
        Assert.Equal(title, book.Title);
        Assert.Equal(author, book.Author);
        Assert.Equal(price, book.Price);
        Assert.Equal(stock, book.Stock);
    }

    [Fact]
    public async Task CreateBook_ShouldThrowBadRequestException_WhenTitleIsTooLong()
    {
        // Arrange
        var title = new string('a', 101); // Title longer than 100 characters
        var author = "Test Author";
        var price = 19.99;
        var stock = 10;
        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _bookService.CreateAsync(title, author, price, stock));
    }

    [Fact]
    public async Task CreateBook_ShouldThrowArgumentException_WhenStockIsLessThanFive()
    {
        // Arrange
        var title = "Test Book";
        var author = "Test Author";
        var price = 19.99;
        var stock = 4; // Stock less than 5
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _bookService.CreateAsync(title, author, price, stock));
    }

    [Fact]
    public async Task GetBooks_ShouldReturnAllBooks()
    {
        // Arrange
       await TestUtilities.SeedDatabaseAsync(typeof(Book));


        var book = await _context.Books.ToListAsync();

        // Act
        var result = await _bookService.GetAllAsync();
        // Assert
        Assert.Equal(2, result.Count());
        result.Should().BeOfType<List<BookDto>>();
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.ForEach(b =>
        {
            b.IsAvailable.Should().Be(b.Stock>0);
            b.Price.Should().BeOfType<string>();
        });
    }
}
