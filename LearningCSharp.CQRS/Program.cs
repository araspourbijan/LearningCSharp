using LearningCSharp.CQRS;
using LearningCSharp.CQRS.Extensions;
using Serilog;
using Serilog.Debugging;
using System.Diagnostics;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);

// Configure Serilog
builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig
        .ReadFrom.Configuration(context.Configuration) // appsetting.json
        .Enrich.WithProperty("ApplicationName", Assembly.GetExecutingAssembly().GetName().Name ?? "CQRSAPI")
        .Enrich.FromLogContext();
});

// Enable Serilog self-logging to Visual Studio Debug output
SelfLog.Enable(msg => Debug.WriteLine(msg));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler(options => { });

app.UseHttpsRedirection();

app.MapEndpoints(Assembly.GetExecutingAssembly());

app.Run();
