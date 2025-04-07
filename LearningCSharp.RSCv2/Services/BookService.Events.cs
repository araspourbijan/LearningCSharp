namespace LearningCSharp.RSCv2.Services;

public partial class BookService
{
    // first approach with EventArgs and delegate type
    public delegate void BookCreatedEventHandler(object source, EmailMessageEventArgs args);
    public event BookCreatedEventHandler BookCreated;

    // second approach with generic EventHandler
    public event EventHandler<DeliveryMessageEventArgs> BookCreatedDelivery;
    public event EventHandler<EmailMessageEventArgs> BookUpdated;
    public event EventHandler<EventArgs> BookDeleted;  //void
    protected virtual void OnBookCreated(EmailMessageEventArgs args) => BookCreated?.Invoke(this, args);
    protected virtual void OnBookCreatedDelivery(DeliveryMessageEventArgs args) => BookCreatedDelivery?.Invoke(this, args);

    protected virtual void OnBookUpdated(EmailMessageEventArgs args) => BookUpdated?.Invoke(this, args);

    protected virtual void OnBookDeleted() => BookDeleted?.Invoke(this, EventArgs.Empty);
}
