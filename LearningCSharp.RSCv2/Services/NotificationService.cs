using LearningCSharp.RSCv2.Services.Interfaces;

namespace LearningCSharp.RSCv2.Services;
using Shared.Enums;

public class EmailMessageEventArgs(EmailTemplateEnum type, string email, string? subject, Guid? id) : EventArgs
{
    public readonly EmailTemplateEnum Type = type;
    public readonly string Email = email;
    public readonly string? Subject = subject;
    public readonly Guid? Id = id;
}

public class NotificationService(ILogger<NotificationService> _logger) : INotificationService
{
    public void OnObjectCreated(object source, EmailMessageEventArgs obj)
    {
        SendEmail(obj);
    }

    public void OnObjectUpdated(object source, EmailMessageEventArgs obj)
    {
        SendEmail(obj);
    }
    private void SendEmail(EmailMessageEventArgs model)
    {
        // send email
        _logger.LogInformation(GetObjectCreationTemplate(model));
    }

    private string GetObjectCreationTemplate(EmailMessageEventArgs model)
    {
        ArgumentNullException.ThrowIfNull(model);

        if (model.Type == EmailTemplateEnum.BookCreated)
            return GetCreateBookEmailTemplate(model);
        else if (model.Type == EmailTemplateEnum.BookModified)
            return GetUpdatedBookEmailTemplate(model);

        return "Object created";
    }

    private string GetCreateBookEmailTemplate(EmailMessageEventArgs model)
    {
        return $"sending email to {model.Email}, subject: {model.Subject} " +
        $"\nMessage: Item with Id: {model.Id} has been created successfully, you can continue ...";
    }

    private string GetUpdatedBookEmailTemplate(EmailMessageEventArgs model)
    {
        return $"sending email to {model.Email}, subject: {model.Subject} " +
        $"\nMessage: Item with Id: {model.Id} has been updated successfully, you can continue ...";
    }
}
