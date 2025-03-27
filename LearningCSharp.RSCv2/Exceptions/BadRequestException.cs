using Shared.Commons;

namespace LearningCSharp.RSCv2.Exceptions;


public class BadRequestException : Exception
{
    private readonly string _message;
    public override string Message => _message ?? Errors.BadRequest.Description;
    public int StatusCode { get; }

    public BadRequestException()
    {
        _message = Errors.BadRequest.Description;
        StatusCode = Errors.BadRequest.Code;
    }

    public BadRequestException(string message) : base(message)
    {
        _message = message;
        StatusCode = Errors.BadRequest.Code;
    }
}
