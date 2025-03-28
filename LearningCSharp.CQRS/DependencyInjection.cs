using FluentValidation;
using LearningCSharp.CQRS.Application.Interfaces;
using LearningCSharp.CQRS.Extensions;
using LearningCSharp.CQRS.Infrastructure;
using LearningCSharp.CQRS.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LearningCSharp.CQRS;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<ApplicationDbContext>(options => options
           .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); // , ?
        services.AddExceptionHandler<GlobalExceptionHandler>();


        return services;
    }
}
