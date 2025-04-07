using LearningCSharp.RSCv2.Services.Interfaces;

namespace LearningCSharp.RSCv2.Services;
using Shared.Enums;

public class EmailMessageArgs : EventArgs
{
    public EmailTemplateEnum Type { get; set; }
    public string Email { get; set; }
    public string? Subject { get; set; }
    public Guid? Id { get; set; }
}

public class NotificationService(ILogger<NotificationService> _logger) : INotificationService
{
    public void OnObjectCreated(object source, EmailMessageArgs obj)
    {
        SendEmail(obj);
    }

    public void OnObjectUpdated(object source, EmailMessageArgs obj)
    {
        SendEmail(obj);
    }
    private void SendEmail(EmailMessageArgs model)
    {
        // send email
        _logger.LogInformation(GetObjectCreationTemplate(model));
    }

    private string GetObjectCreationTemplate(EmailMessageArgs model)
    {
        ArgumentNullException.ThrowIfNull(model);

        if (model.Type == EmailTemplateEnum.BookCreated)
            return GetCreateBookEmailTemplate(model);

        return "Object created";
    }

    private string GetCreateBookEmailTemplate(EmailMessageArgs model)
    {
        return $"sending email to {model.Email}, subject: {model.Subject} " +
        $"\nMessage: Item with Id: {model.Id} has been created successfully, you can continue ...";
    }
}
