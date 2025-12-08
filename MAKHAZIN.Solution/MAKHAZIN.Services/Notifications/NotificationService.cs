using MAKHAZIN.Core;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Hubs.Notifications;
using MAKHAZIN.Core.Services.Contract;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IHubContext<NotificationHub, INotificationClient> hubContext, IUnitOfWork unitOfWork)
        {
            _hubContext = hubContext;
            _unitOfWork = unitOfWork;
        }
        public async Task SendNotificationToUserAsync(int userId, string title, string message)
        {
            // Save notification to database
            var notification = new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.Repository<Notification>().AddAsync(notification);
            await _unitOfWork.CompleteAsync();

            // Send notification via SignalR
            var dto = new NotificationDTO
            {
                UserId = notification.UserId,
                Title = notification.Title,
                Message = notification.Message,
                CreatedAt = notification.CreatedAt
            };
            await _hubContext.Clients.User(userId.ToString()).ReceiveNotification(dto);
        }
    }
}
