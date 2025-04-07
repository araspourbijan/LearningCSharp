namespace LearningCSharp.RSCv2.Services.Interfaces;

public interface IDeliveryService
{
    public void OnObjectCreated(object source, DeliveryMessageArgs obj);
    public void OnObjectDeleted(object source);
}
