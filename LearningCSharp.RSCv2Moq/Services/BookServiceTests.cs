using AutoMapper;
using FluentAssertions;
using LearningCSharp.RSCv2.Repositories;
using LearningCSharp.RSCv2.Services;
using LearningCSharp.RSCv2.Services.Interfaces;
using Moq;
using Shared.Dtos;
using Shared.Exceptions;
using Shared.Models;

namespace LearningCSharp.RSCv2Moq.Services;
public class BookServiceTests
{
    private readonly Mock<IRepository<Book>> _bookRepositoryMock;
    private readonly IBookService _bookService;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<INotificationService> _notificationServiceMock;
    private readonly Mock<IDeliveryService> _deliveryServiceMock;
    public BookServiceTests()
    {
        _bookRepositoryMock = new Mock<IRepository<Book>>();
        _mapper = new Mock<IMapper>();
        _notificationServiceMock = new Mock<INotificationService>();
        _deliveryServiceMock = new Mock<IDeliveryService>();
        _bookService = new BookService(
            _bookRepositoryMock.Object,
            _mapper.Object,
            _notificationServiceMock.Object,
            _deliveryServiceMock.Object
            );
    }

    #region CreateBook
    [Theory]
    [InlineData("Book Title", "Book Author", 10, 2)]
    [InlineData("Another Book", "Another Author", 15, 4)]
    public async Task CreateBook_ShouldThrowArgumentException_WhenStockIsLessThan5(string title, string author, double price, int stock)
    {
        // Arrange
        // Act
        Func<Task> act = async () => await _bookService.CreateAsync(title, author, price, stock);
        // Assert
        await act.Should()
            .ThrowAsync<ArgumentException>()
            .WithMessage("Stock must be at least 5 (Parameter 'stock')");
    }

    [Theory]
    [InlineData("Book Title.....................................................................................................", "Book Author", 10, 5)]
    [InlineData("Another Book....................................................................................................", "Another Author", 15, 10)]
    public async Task CreateBook_ShouldThrowBadRequestException_WhenTitleIsTooLong(string title, string author, double price, int stock)
    {
        // Arrange
        // Act
        Func<Task> act = async () => await _bookService.CreateAsync(title, author, price, stock);
        // Assert
        await act.Should()
            .ThrowAsync<BadRequestException>()
            .WithMessage("Title is too long");
    }

    [Theory]
    [InlineData("Book Title", "Book Author", 10, 5)]
    [InlineData("Another Book", "Another Author", 15, 10)]
    public async Task CreateBook_ShouldSucceed_WhenValidDataIsProvided(string title, string author, double price, int stock)
    {
        // Arrange
        _bookRepositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<Book>()))
            .Returns(Task.CompletedTask);

        // Act
        Func<Task> act = async () => await _bookService.CreateAsync(title, author, price, stock);

        // Assert
        await act.Should().NotThrowAsync();
        _bookRepositoryMock.Verify(repo => repo.CreateAsync(It.Is<Book>(b =>
            b.Title == title &&
            b.Author == author &&
            b.Price == price &&
            b.Stock == stock
        )), Times.Once);
    }
    #endregion

    #region GetBookById

    [Fact]
    public async Task GetBookById_ShouldThrowNotFoundException_WhenBookDoesNotExist()
    {
        // Arrange
        var bookId = Guid.NewGuid();

        _bookRepositoryMock
            .Setup(repo => repo.GetByIdAsync(bookId))
            .ThrowsAsync(new NotFoundException($"Book with ID {bookId} not found"));

        // Act
        Func<Task> act = async () => await _bookService.GetByIdAsync(bookId);

        // Assert
        await act.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage($"Book with ID {bookId} not found");
    }

    [Fact]
    public async Task GetBookById_ShouldReturnBook_WhenBookExists()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var expectedBook = new Book { Id = bookId, Title = "Test Book", Stock = 3 };
        var expectedBookDto = new BookDto { Id = bookId, Title = "Test Book", Stock = 2 };

        _bookRepositoryMock
            .Setup(repo => repo.GetByIdAsync(bookId))
            .ReturnsAsync(expectedBook);

        _mapper
            .Setup(m => m.Map<BookDto>(expectedBook))
            .Returns(expectedBookDto);

        // Act
        var result = await _bookService.GetByIdAsync(bookId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BookDto>();
        result.Id.Should().Be(expectedBook.Id);
        result.Title.Should().Be(expectedBook.Title);
        result.IsAvailable.Should().Be(expectedBook.Stock > 0);
    }
    #endregion

    #region GetAllBooks
    [Fact]
    public async Task GetAllBooks_ShouldReturnListOfBooks()
    {
        // Arrange
        List<Book> books = [
            new () { Id = Guid.NewGuid(), Title = "Book 1", Stock = 5 },
            new () { Id = Guid.NewGuid(), Title = "Book 2", Stock = 0 }
        ];

        List<BookDto> bookDtos = [
             new () { Id = books[0].Id, Title = books[0].Title, Stock = 5  },
             new () { Id = books[1].Id, Title = books[1].Title,  Stock = 0 }
        ];

        _bookRepositoryMock
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(books);
        _mapper
            .Setup(m => m.Map<List<BookDto>>(books))
            .Returns(bookDtos);
        // Act
        var result = await _bookService.GetAllAsync();
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<BookDto>>();
        result.Count.Should().Be(2);
        result[0].Id.Should().Be(books[0].Id);
    }

    [Fact]
    public async Task GetAllBooks_ShouldReturnEmptyList_WhenNoBooksExist()
    {
        // Arrange
        List<Book> books = new();
        List<BookDto> bookDtos = new();

        _bookRepositoryMock
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(books);
        _mapper
            .Setup(m => m.Map<List<BookDto>>(books))
            .Returns(bookDtos);
        // Act
        var result = await _bookService.GetAllAsync();
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<BookDto>>();
        result.Count.Should().Be(0);
    }
    #endregion

    #region UpdateBook
    [Fact]
    public async Task UpdateBook_ShouldThrowNotFoundException_WhenBookDoesNotExist()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var bookToUpdate = new Book { Id = bookId, Title = "Updated Book", Stock = 5 };
        _bookRepositoryMock
            .Setup(repo => repo.UpdateAsync(bookId, bookToUpdate))
            .ThrowsAsync(new BadRequestException($"Book with ID {bookId} not found"));
        // Act
        Func<Task> act = async () => await _bookService.UpdateAsync(bookId, bookToUpdate);
        // Assert
        await act.Should()
            .ThrowAsync<BadRequestException>()
            .WithMessage($"Book with ID {bookId} not found");
    }

    [Fact]
    public async Task UpdateBook_ShouldSucceed_WhenValidDataIsProvided()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        var bookToUpdate = new Book { Id = bookId, Title = "Updated Book", Stock = 5 };
        _bookRepositoryMock
            .Setup(repo => repo.UpdateAsync(bookId, bookToUpdate))
            .Returns(Task.CompletedTask);
        // Act
        Func<Task> act = async () => await _bookService.UpdateAsync(bookId, bookToUpdate);
        // Assert
        await act.Should().NotThrowAsync();
        _bookRepositoryMock.Verify(repo => repo.UpdateAsync(bookId, bookToUpdate), Times.Once);
    }
    #endregion

    #region DeleteBook
    [Fact]
    public async Task DeleteBook_ShouldThrowNotFoundException_WhenBookDoesNotExist()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        _bookRepositoryMock
            .Setup(repo => repo.DeleteAsync(bookId))
            .ThrowsAsync(new NotFoundException($"Book with ID {bookId} not found"));
        // Act
        Func<Task> act = async () => await _bookService.DeleteAsync(bookId);
        // Assert
        await act.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage($"Book with ID {bookId} not found");
    }
    [Fact]
    public async Task DeleteBook_ShouldSucceed_WhenBookExists()
    {
        // Arrange
        var bookId = Guid.NewGuid();
        _bookRepositoryMock
            .Setup(repo => repo.DeleteAsync(bookId))
            .Returns(Task.CompletedTask);
        // Act
        Func<Task> act = async () => await _bookService.DeleteAsync(bookId);
        // Assert
        await act.Should().NotThrowAsync();
        _bookRepositoryMock.Verify(repo => repo.DeleteAsync(bookId), Times.Once);
    }
    #endregion
}
