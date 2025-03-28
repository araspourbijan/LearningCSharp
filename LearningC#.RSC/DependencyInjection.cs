using LearningCSharp.RSC.Infrastructure;
using LearningCSharp.RSC.Repositories;
using LearningCSharp.RSC.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LearningCSharp.RSC;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<ApplicationDbContext>(options =>options
            .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(BookService));

       services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
