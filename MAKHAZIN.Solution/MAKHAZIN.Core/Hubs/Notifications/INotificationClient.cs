using MAKHAZIN.Core.DTOs;

namespace MAKHAZIN.Core.Hubs.Notifications
{
    public interface INotificationClient
    {
        Task ReceiveNotification(NotificationDTO notification);
    }
}
