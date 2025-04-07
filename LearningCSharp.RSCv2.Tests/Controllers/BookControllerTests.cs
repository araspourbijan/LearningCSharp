using FakeItEasy;
using FluentAssertions;
using LearningCSharp.RSCv2.Controllers;
using LearningCSharp.RSCv2.Services.Interfaces;
using Shared.Dtos;

namespace LearningCSharp.RSCv2.Tests.Controllers;
public class BookControllerTests
{
    private readonly IBookService _bookService;
    private readonly BookController _bookController;
    public BookControllerTests()
    {
        _bookService = A.Fake<IBookService>();
        _bookController = new BookController(_bookService);
    }

    [Fact]
    public async Task BookController_GetBooksAsync_ShouldReturnListOfBooks()
    {

        // Arrange
        List<BookDto> books = [
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Book Title",
                Author = "Book Author",
                Price = "10.99 €",
                Stock = 5
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Another Book Title",
                Author = "Another Book Author",
                Price = "15.99 €",
                Stock = 10
            }
            ];

        A.CallTo(() => _bookService.GetAllAsync()).Returns(Task.FromResult(books));

        // Act
        var output = await _bookController.GetBooksAsync();

        // Assert
        Assert.Equal(books, output);
        output.Should().BeOfType<List<BookDto>>();
        output.Count.Should().Be(books.Count);
    }

    [Theory]
    [InlineData("{5EEE07E7-9605-4C8E-8E15-E451E7AAD93D}", "Book Title", "Book Author", 10.99, 5)]
    [InlineData("{3CE00D4A-EAFC-4800-9092-9CA169D7CB0B}", "Another Book Title", "Another Book Author", 15.99, 10)]

    public async Task BookController_GetBookByIdAsync_ShouldReturnBook(Guid id, string title, string author, double price, int stock)
    {
        // Arrange
        var book = A.Fake<BookDto>();
        book.Id = id;
        book.Title = title;
        book.Author = author;
        book.Price = price + " €";
        book.Stock = stock;

        A.CallTo(() => _bookService.GetByIdAsync(id)).Returns(book);

        // Act
        var output = await _bookController.GetBookByIdAsync(id);

        // Assert
        Assert.Equal(book, output);
        output.Should().BeAssignableTo<BookDto>().And.BeSameAs(book);
    }
}
