using System.Reflection;

namespace LearningCSharp.CQRS.Extensions;

public static class WebApplicationExtensions
{
    public static RouteGroupBuilder MapGroup(this WebApplication app, EndpointGroupBase group)
    {
        string name = group.GetType().Name;
        return app.MapGroup("/api/" + name).WithTags(name)/*.WithOpenApi()*/;
    }

    public static WebApplication MapEndpoints(this WebApplication app, Assembly assembly)
    {
        Type endpointGroupType = typeof(EndpointGroupBase);

        foreach (Type item in from t in assembly.GetExportedTypes()
                              where t.IsSubclassOf(endpointGroupType)
                              select t)
            if (Activator.CreateInstance(item) is EndpointGroupBase endpointGroupBase)
                endpointGroupBase.Map(app);

        return app;
    }
}
