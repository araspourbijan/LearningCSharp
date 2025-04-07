using FluentAssertions;
using LearningCSharp.RSCv2.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using static LearningCSharp.RSCv2.Tests.TestUtilities;

namespace LearningCSharp.RSCv2.Tests.Repositories;
public class BookRepository
{

    [Fact]
    public async Task AddBookAsync()
    {
        //arrange
        using var context = await SeedDatabaseAsync(typeof(Book));
        IRepository<Book> _repository = new GenericRepository<Book>(context);

        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = "New Book",
            Author = "New Author",
            Price = 20.0,
            Stock = 10
        };

        //act
        await _repository.CreateAsync(book);
        var result = await context.Books.FindAsync(book.Id);

        //assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Book>();
        result.Should().BeEquivalentTo(book);
    }

    [Fact]
    public async Task GetBooksAsync_ShouldReturnListOfBooks()
    {
        using var context = await SeedDatabaseAsync(typeof(Book));

        var books = await context.Books.ToListAsync();

        books.Should().BeOfType<List<Book>>();
        books.Count.Should().Be(context.Books.Count());
    }

    [Fact]
    public async Task GetBookByIdAsync_ShouldReturnBook()
    {
        using var context = await SeedDatabaseAsync(typeof(Book));
        IRepository<Book> _repository = new GenericRepository<Book>(context);
        var book = await context.Books.FirstOrDefaultAsync();

        book.Should().NotBeNull();

        var output = await _repository.GetByIdAsync(book.Id);

        output.Should().NotBeNull();
        output.Should().BeOfType<Book>();
        output.Should().BeEquivalentTo(book);
    }

    [Fact]
    public async Task UpdateBookAsync_ShouldUpdateBook()
    {
        using var context = await SeedDatabaseAsync(typeof(Book));
        IRepository<Book> _repository = new GenericRepository<Book>(context);
        var book = await context.Books.FirstOrDefaultAsync();
        book.Should().NotBeNull();

        book.Title = "Updated Title";
        book.Author = "Updated Author";
        book.Price = 25.0;
        book.Stock = 15;

        var res = _repository.UpdateAsync(book.Id, book);
        res.Should().NotBeNull();
        res.IsCompleted.Should().BeTrue();

        var updatedBook = await context.Books.FindAsync(book.Id);
        updatedBook.Should().NotBeNull();
        updatedBook.Should().BeOfType<Book>();
        updatedBook.Title.Should().Be("Updated Title");
        updatedBook.Author.Should().Be("Updated Author");
        updatedBook.Price.Should().Be(25.0);
        updatedBook.Stock.Should().Be(15);
    }

    [Fact]
    public async Task DeleteBookAsync_ShouldDeleteBook()
    {
        using var context = await SeedDatabaseAsync(typeof(Book));
        IRepository<Book> _repository = new GenericRepository<Book>(context);

        var book = await context.Books.FirstOrDefaultAsync();
        book.Should().NotBeNull();

        var res = _repository.DeleteAsync(book.Id);
        res.Should().NotBeNull();
        res.IsCompleted.Should().BeTrue();

        var deletedBook = await context.Books.FindAsync(book.Id);
        deletedBook.Should().BeNull();
    }
}
