namespace SharedModel.Models;
public class Book : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public double Price { get; set; }
    public bool IsAvailable { get; set; }
}
