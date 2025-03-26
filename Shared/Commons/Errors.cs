namespace Shared.Commons;

public sealed record ErrorRecord(int Code, string Description);
public static class Errors
{
    public static readonly ErrorRecord None = new(0, string.Empty);
    public static readonly ErrorRecord BadRequest = new(400, "Bad Request");
    public static readonly ErrorRecord NullArgument = new(400, "Argument Null Exception");
    public static readonly ErrorRecord Unauthorized = new(401, "Unauthorized");
    public static readonly ErrorRecord Forbidden = new(403, "Forbidden");
    public static readonly ErrorRecord Conflict = new(409, "Conflict");
    public static readonly ErrorRecord UnsupportedMediaType = new(415, "Unsupported Media Type");
    public static readonly ErrorRecord NotImplemented = new(501, "Not Implemented");
    public static readonly ErrorRecord ServiceUnavailable = new(503, "Service Unavailable");
    public static ErrorRecord NotFound(Guid id) => new(404, $"The item with Id '{id}' was not found");
}
