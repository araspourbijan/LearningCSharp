namespace Shared.Models;
public class Book : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Stock { get; set; }
    public Guid? UserId { get; set; }
    public AppUser? User { get; set; }
}
