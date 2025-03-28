using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace LearningCSharp.CQRS.Application.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Book> Books { get; }
    public DbSet<AppUser> Users { get; }
    public Task SaveChangesAsync();
}
