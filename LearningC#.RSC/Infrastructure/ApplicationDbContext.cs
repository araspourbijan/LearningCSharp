using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace LearningCSharp.RSC.Infrastructure;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<AppUser> Users => Set<AppUser>();
}
