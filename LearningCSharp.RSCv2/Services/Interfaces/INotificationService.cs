namespace LearningCSharp.RSCv2.Services.Interfaces;

public interface INotificationService
{
    public void OnObjectCreated(object source, EmailMessageArgs obj);
    public void OnObjectUpdated(object source, EmailMessageArgs obj);
}
