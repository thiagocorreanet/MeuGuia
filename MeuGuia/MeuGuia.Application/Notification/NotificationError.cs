using MeuGuia.Domain.Interface;

namespace MeuGuia.Application.Notification;

public class NotificationError : INotificationError
{
    private readonly List<MeuGuia.Domain.Notification.NotificationError> _notifications;

    public NotificationError()
    {
        _notifications = new List<Domain.Notification.NotificationError>();
    }

    /// <summary>
    /// Obtém a lista de notificações de erro.
    /// </summary>
    /// <returns>Uma lista contendo as notificações de erro.</returns>
    public List<Domain.Notification.NotificationError> GetNotifications()
    {
        return _notifications;
    }

    /// <summary>
    /// Adiciona uma notificação de erro à lista.
    /// </summary>
    /// <param name="notification">Notificação de erro a ser adicionada.</param>
    public void Handle(Domain.Notification.NotificationError notification)
    {
        _notifications.Add(notification);
    }

    /// <summary>
    /// Verifica se há notificações de erro.
    /// </summary>
    /// <returns>Verdadeiro se houver notificações, falso caso contrário.</returns>
    public bool HasNotifications()
    {
        return _notifications.Any();
    }
}
