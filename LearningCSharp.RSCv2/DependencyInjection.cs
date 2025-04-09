using LearningCSharp.RSCv2.Infrastructure;
using LearningCSharp.RSCv2.Middleware;
using LearningCSharp.RSCv2.Repositories;
using LearningCSharp.RSCv2.Services;
using LearningCSharp.RSCv2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LearningCSharp.RSCv2;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<ApplicationDbV2Context>(options =>
            options
            .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            //.UseInMemoryDatabase("InMemoryDb")
            );

        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(BookService));
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IDeliveryService, DeliveryService>();
        services.AddScoped<IBookService, BookService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddMemoryCache();

        services.AddResponseCaching();
        //services.AddOutputCache(options =>
        //{
        //    options.AddBasePolicy(builder =>
        //    {
        //        builder.Expire(TimeSpan.FromSeconds(30));
        //    });
        //});

        return services;
    }
}
