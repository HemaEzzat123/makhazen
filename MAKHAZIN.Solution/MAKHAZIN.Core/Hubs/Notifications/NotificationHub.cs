using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Hubs.Notifications
{
    public class NotificationHub : Hub<INotificationClient>
    {
        //public async Task SendNotificationToUser(int userId, string title, string message)
        //{
        //    await Clients.User(userId.ToString()).ReceiveNotification(new DTOs.NotificationDTO
        //    {
        //        UserId = userId,
        //        Title = title,
        //        Message = message,
        //        CreatedAt = DateTime.UtcNow
        //    });
        //}
    }
}
