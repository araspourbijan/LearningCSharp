namespace LearningCSharp.CQRS.Extensions;

public abstract class EndpointGroupBase
{
    public virtual string Endpoint { get; } = string.Empty;
    public abstract void Map(WebApplication app);
}
