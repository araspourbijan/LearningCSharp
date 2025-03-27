using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace LearningCSharp.RSCv2.Infrastructure;
public class ApplicationDbV2Context(DbContextOptions<ApplicationDbV2Context> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<AppUser> Users => Set<AppUser>();
}
