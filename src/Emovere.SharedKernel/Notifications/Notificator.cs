namespace Emovere.SharedKernel.Notifications
{
    public sealed class Notificator : INotificator
    {
        private readonly List<Notification> _notifications = [];

        public void ClearNotifications() => _notifications.Clear();

        public List<Notification> GetNotifications() => _notifications;

        public void HandleNotification(Notification notification) => _notifications.Add(notification);

        public bool HasNotifications() => _notifications.Count > 0;
    }
}