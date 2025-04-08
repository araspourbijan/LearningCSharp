using Shared.Dtos;
using System.Text.Json;
using System.Text.Json.Serialization;

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

//[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
//[JsonSerializable(typeof(List<BookDto>))]
//[JsonSerializable(typeof(BookDto))]
//public partial class CacheSourceGenerationContext : JsonSerializerContext
//{
//}
