namespace LearningCSharp.RSCv2.Services.Interfaces;

public interface INotificationService
{
    public void OnObjectCreated(object source, EmailMessageEventArgs obj);
    public void OnObjectUpdated(object source, EmailMessageEventArgs obj);
}
