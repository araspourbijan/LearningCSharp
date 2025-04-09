using LearningCSharp.CQRS;
using LearningCSharp.CQRS.Extensions;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using System.Diagnostics;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);


// Configure Serilog
builder.Logging.ClearProviders();
builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("System", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Debug ) // <- EF Core queries
        .Enrich.WithProperty("ApplicationName", Assembly.GetExecutingAssembly().GetName().Name ?? "CQRSAPI")
        .Enrich.FromLogContext()
        .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
        .WriteTo.Debug(restrictedToMinimumLevel: LogEventLevel.Debug)
        .WriteTo.File("logs/myApp.txt", restrictedToMinimumLevel: LogEventLevel.Warning, rollingInterval: RollingInterval.Day)
        .WriteTo.Seq("http://localhost:5341", restrictedToMinimumLevel: LogEventLevel.Warning);
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
