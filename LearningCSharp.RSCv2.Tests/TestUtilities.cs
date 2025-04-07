using LearningCSharp.RSCv2.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace LearningCSharp.RSCv2.Tests;
public static class TestUtilities
{
    public static ApplicationDbV2Context GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbV2Context>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var context = new ApplicationDbV2Context(options);

        return context;
    }

    public static async Task<ApplicationDbV2Context> SeedDatabaseAsync(Type type)
    {
        var context = GetDbContext();

        if (type == typeof(Book))
        {
            await SeedBooks(context);
        }
        return context;
    }

    private static async Task SeedBooks(ApplicationDbV2Context context)
    {
        await context.Database.EnsureCreatedAsync();
        if (!await context.Books.AnyAsync())
        {
            for (int i = 0; i < 10; i++)
            {
                context.Add(new Book
                {
                    Id = Guid.NewGuid(),
                    Title = $"Book {i}",
                    Author = $"Author {i}",
                    Price = i * 10 + .5,
                    Stock = i + 1 * 5,
                });
            }
            await context.SaveChangesAsync();
        }
    }
}
