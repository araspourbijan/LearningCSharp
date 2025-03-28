using LearningCSharp.CQRS.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace LearningCSharp.CQRS.Infrastructure;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<AppUser> Users => Set<AppUser>();

    public async Task SaveChangesAsync()
    {
        await base.SaveChangesAsync();
    }
}
