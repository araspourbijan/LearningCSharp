namespace LearningCSharp.RSCv2.Services.Interfaces;

public interface IDeliveryService
{
    public void OnObjectCreated(object source, DeliveryMessageEventArgs obj);
    public void OnObjectDeleted(object source, EventArgs args);
}
