namespace LearningCSharp.RSCv2.Services;

//public record ObjectCreatedEmailArgs(string Email, string? Subject, Guid Id, string Type, int Stock);// non mi crea dei problemi
public class DeliveryMessageArgs : EventArgs
{
    public string Type { get; set; }
    public int Stock { get; set; }
    public Guid Id { get; set; }
    public string Title { get; set; }
}
