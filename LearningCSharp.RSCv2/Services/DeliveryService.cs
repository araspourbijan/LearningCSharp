using LearningCSharp.RSCv2.Services.Interfaces;

namespace LearningCSharp.RSCv2.Services;

public class DeliveryMessageEventArgs(Guid id, string title, int stock, string type) : EventArgs
{
    public readonly Guid Id = id;
    public readonly string Title = title;
    public readonly int Stock = stock;
    public readonly string Type = type;
}

public class DeliveryService(ILogger<DeliveryService> _logger) : IDeliveryService
{
    public void OnObjectCreated(object source, DeliveryMessageEventArgs obj)
    {
        _logger.LogInformation($"Object Ordered: {obj.Title} with id {obj.Id}\n{obj.Stock} items are waiting to delivered to library");
    }

    public void OnObjectDeleted(object source, EventArgs args)
    {
        _logger.LogInformation("\nItem has been deleted");
    }
}
