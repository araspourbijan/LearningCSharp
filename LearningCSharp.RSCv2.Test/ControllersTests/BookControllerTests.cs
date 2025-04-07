using FakeItEasy;
using LearningCSharp.RSCv2.Controllers;
using LearningCSharp.RSCv2.Services.Interfaces;
using Shared.Exceptions;

namespace LearningCSharp.RSCv2.Test.ControllersTests;
public class BookControllerTests
{
    private readonly IBookService _IBookService;
    private readonly BookController _controller;

    public BookControllerTests()
    {
        _IBookService = A.Fake<IBookService>();
        _controller = new(_IBookService);
    }

    [Theory]
    [InlineData("This is a very long title that is more than 100 characters long lorem ipsum param param param ************************************************")]
    [InlineData("lorem ipsum, lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum param ***************************************")]
    public async Task CreateAsync_WhenTitleIsTooLong_ThrowsBadRequestException(string param)
    {
        // Arrange
        var title = param;
        var author = "Author";
        var price = 10.0;
        var stock = 10;

        // book service throw validation exception
        A.CallTo(() => _IBookService.CreateAsync(title, author, price, stock))
            .Throws(new BadRequestException("Title is too long"));

        // Act
        async Task Act() => await _controller.CreateBookAsync(title, author, price, stock);
        // Assert
        var result = await Assert.ThrowsAsync<BadRequestException>(Act);
        Assert.Equal("Title is too long", result.Message);
        Assert.True(result.StatusCode == 400);
    }
}
