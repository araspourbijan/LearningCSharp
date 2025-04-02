using AutoMapper;
using LearningCSharp.RSCv2.Repositories;
using LearningCSharp.RSCv2.Services;
using Moq;
using Shared.Dtos;
using Shared.Exceptions;
using Shared.Models;

namespace LearningCSharp.RSCv2.Test;
public class BookServiceTest
{
    private readonly Mock<IRepository<Book>> _mockRepository;
    private readonly BookService _BookService;
    private readonly Mock<IMapper> _mapper;

    public BookServiceTest()
    {
        _mockRepository = new Mock<IRepository<Book>>();
        _mapper = new Mock<IMapper>();
        _BookService = new BookService(_mockRepository.Object, _mapper.Object);
    }

    #region CreateAsync

    [Theory]
    [InlineData("This is a very long title that is more than 100 characters long lorem ipsum param param param *************************")]
    [InlineData("lorem ipsum, lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum param *************** ")]
    public async Task CreateAsync_WhenTitleIsTooLong_ThrowsBadRequestException(string param)
    {
        // Arrange
        var title = param;
        var author = "Author";
        var price = 10.0;
        var stock = 10;
        // Act
        async Task Act() => await _BookService.CreateAsync(title, author, price, stock);
        // Assert
        var result = await Assert.ThrowsAsync<BadRequestException>(Act);
        Assert.Equal("Title is too long", result.Message);
        Assert.True(result.StatusCode == 400);
    }

    [Theory]
    [InlineData("Title", "Author", 10.0, 3)]
    [InlineData("Tom & Gerry", "Tom", 12.0, 4)]
    public async Task CreateAsync_WhenStockISLessThan5_ThrowsArgumentException(string title, string author, double price, int stock)
    {

        async Task Act() => await _BookService.CreateAsync(title, author, price, stock);

        await Assert.ThrowsAsync<ArgumentException>(nameof(stock), Act);
    }

    [Theory]
    [InlineData("Title", "Author", 10.0, 10)]
    [InlineData("Tom & Gerry", "Tom", 12.0, 50)]
    public async Task CreateAsync_WhenTitleIsValid_CreatesBook(string title, string author, double price, int stock)
    {
        await _BookService.CreateAsync(title, author, price, stock);

        _mockRepository.Verify(x => x.CreateAsync(It.IsAny<Book>()), Times.Once);
    }
    #endregion

    #region GetAllAsync

    [Fact]
    public async Task GetAllAsync_Return_ListOfBooksDto()
    {
        var result = await _BookService.GetAllAsync();
        Assert.NotNull(result);
        Assert.IsType<List<BookDto>>(result);
    }
    #endregion
}
