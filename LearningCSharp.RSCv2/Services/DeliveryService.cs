using LearningCSharp.RSCv2.Services.Interfaces;

namespace LearningCSharp.RSCv2.Services;

public class DeliveryService(ILogger<DeliveryService> _logger) : IDeliveryService
{
    public void OnObjectCreated(object source, DeliveryMessageArgs obj)
    {
        _logger.LogInformation($"Object Ordered: {obj.Title} with id {obj.Id}\n{obj.Stock} items are waiting to delivered to library");
    }
}
