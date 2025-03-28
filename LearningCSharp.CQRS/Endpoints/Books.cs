using LearningCSharp.CQRS.Application.Books.Commands;
using LearningCSharp.CQRS.Application.Books.Queries;
using LearningCSharp.CQRS.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace LearningCSharp.CQRS.Endpoints;

public class Books : EndpointGroupBase
{
    public override string Endpoint { get; } = "book";
    public override void Map(WebApplication app)
    {
        app.MapGroup(this).MapPost("/", AddBook);
        app.MapGroup(this).MapGet("/list", GetBooks);
        app.MapGroup(this).MapGet("/{id:guid}", GetBookById);
        app.MapGroup(this).MapPut("/{id:guid}", UpdateBook);
        app.MapGroup(this).MapDelete("/{id:guid}", DeleteBook);
    }

    public async Task AddBook(ISender sender, AddBookCommand command)
    {
        await sender.Send(command);
    }
    public async Task<List<BookDto>> GetBooks(ISender sender)
    {
        return await sender.Send(new GetBooksQuery());
    }
    public async Task<BookDto> GetBookById(ISender sender, [FromRoute] Guid id)
    {
        return await sender.Send(new GetBookByIdQuery(id));
    }

    public async Task UpdateBook(ISender sender, [FromRoute] Guid id, [FromBody] UpdateBookCommand command)
    {
        command = command with { Id = id };
        await sender.Send(command);
    }

    public async Task DeleteBook(ISender sender, [FromRoute] Guid id)
    {
        await sender.Send(new DeleteBookCommand(id));
    }
}
