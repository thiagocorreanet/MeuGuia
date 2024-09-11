namespace MeuGuia.Domain.Notification;

public class NotificationError
{
    public NotificationError(string message)
    {
        Message = message;
    }

    public string Message { get; }
}
