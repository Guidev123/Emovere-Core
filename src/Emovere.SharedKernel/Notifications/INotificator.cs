namespace Emovere.SharedKernel.Notifications
{
    public interface INotificator
    {
        bool HasNotifications();

        List<Notification> GetNotifications();

        void ClearNotifications();

        void HandleNotification(Notification notification);
    }
}