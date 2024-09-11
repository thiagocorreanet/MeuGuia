using MeuGuia.Domain.Notification;

namespace MeuGuia.Domain.Interface;

public interface INotificationError
{
    void Handle(NotificationError notification);
    List<NotificationError> GetNotifications();
    bool HasNotifications();
}
