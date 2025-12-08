using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Services.Contract
{
    public interface INotificationService
    {
        Task SendNotificationToUserAsync(int userId, string title, string message);
    }
}
