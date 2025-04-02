namespace LearningCSharp.RSCv2.Services;

//public record ObjectCreatedEmailArgs(string Email, string? Subject, Guid Id, string Type, int Stock);// non mi crea dei problemi
public class EmailMessageArgs : EventArgs
{
    public string Type { get; set; }
    public string Email { get; set; }
    public string? Subject { get; set; }
    public Guid? Id { get; set; }
}
