using Shared.Commons;

namespace Shared.Exceptions;

public class NotFoundException : Exception
{
    public int StatusCode { get; }

    public NotFoundException(Guid id) : base(Errors.NotFound(id).Description)
    {
        StatusCode = Errors.NotFound(id).Code;
    }

    public NotFoundException(string message) : base(message)
    {
        StatusCode = Errors.NotFound(Guid.Empty).Code;
    }
}
