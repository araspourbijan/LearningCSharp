namespace SharedModel.Models;
public class AppUser
{
    public string Name { get; set; }
    public List<Book> Books { get; set; } = [];
}
