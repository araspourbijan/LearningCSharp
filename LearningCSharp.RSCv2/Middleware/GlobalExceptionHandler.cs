using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions;
using System.Net;

namespace LearningCSharp.RSCv2.Middleware;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
      HttpContext httpContext,
      Exception exception,
      CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unhandled exception occurred.");

        var statusCode = exception switch
        {
            BadRequestException => (int)HttpStatusCode.BadRequest,
            NullException => (int)HttpStatusCode.BadRequest,
            NotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError 
        };

        var problemDetails = new ProblemDetails
        {
            Title = "An error occurred",
            Status = statusCode,
            Detail = exception.Message,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
            Type = "https://httpstatuses.com/" + statusCode
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}