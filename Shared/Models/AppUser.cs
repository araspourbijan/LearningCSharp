namespace Shared.Models;
public class AppUser : BaseEntity
{
    public string Name { get; set; }
    public List<Book> Books { get; set; } = [];
}
