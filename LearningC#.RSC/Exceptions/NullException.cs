using Shared.Commons;

namespace LearningCSharp.RSC.Exceptions;

public class NullException : Exception
{
    private readonly string _message;
    public override string Message => _message ?? Errors.NotFound.Description;

    public NullException()
    {
        _message = Errors.NotFound.Description;
    }

    public NullException(string message) : base(message)
    {
        _message = message;
    }
}
