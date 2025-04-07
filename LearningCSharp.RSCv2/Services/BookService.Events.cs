namespace LearningCSharp.RSCv2.Services;

public partial class BookService
{
    //public delegate void BookCreatedEventHandler(object source, EventArgs args);
    public delegate void BookCreatedEventHandler(object source, EmailMessageArgs args);

    public event BookCreatedEventHandler BookCreated; // first approach with EventArgs and delegate type
    public event EventHandler<DeliveryMessageArgs> BookCreatedDelivery; // second approach with generic EventHandler
    public event EventHandler<EmailMessageArgs> BookUpdated; // second approach with generic EventHandler
    public event EventHandler BookDeleted; // third approach with normal EventHandler
    protected virtual void OnBookCreated(EmailMessageArgs args)
    {
        BookCreated?.Invoke(this, args);
    }
    protected virtual void OnBookCreatedDelivery(DeliveryMessageArgs args)
    {
        BookCreatedDelivery?.Invoke(this, args);
    }

    protected virtual void OnBookUpdated(EmailMessageArgs args)
    {
        BookUpdated?.Invoke(this, args);
    }

    protected virtual void OnBookDeleted()
    {
        BookDeleted?.Invoke(this, EventArgs.Empty);
    }
}
