using Shared.Commons;

namespace Shared.Exceptions;

public class NullException : Exception
{
    private readonly string _message;
    public override string Message => _message ?? Errors.NullArgument.Description;

    public NullException()
    {
        _message = Errors.NullArgument.Description;
    }

    public NullException(string message) : base(message)
    {
        _message = message;
    }
}
