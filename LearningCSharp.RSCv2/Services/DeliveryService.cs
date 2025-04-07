using LearningCSharp.RSCv2.Services.Interfaces;

namespace LearningCSharp.RSCv2.Services;

public class DeliveryMessageArgs : EventArgs
{
    public string Type { get; set; }
    public int Stock { get; set; }
    public Guid Id { get; set; }
    public string Title { get; set; }
}

public class DeliveryService(ILogger<DeliveryService> _logger) : IDeliveryService
{
    public void OnObjectCreated(object source, DeliveryMessageArgs obj)
    {
        _logger.LogInformation($"Object Ordered: {obj.Title} with id {obj.Id}\n{obj.Stock} items are waiting to delivered to library");
    }

    public void OnObjectDeleted(object source)
    {
        _logger.LogInformation($"Object Deleted: Item has been deleted");
    }
}
 