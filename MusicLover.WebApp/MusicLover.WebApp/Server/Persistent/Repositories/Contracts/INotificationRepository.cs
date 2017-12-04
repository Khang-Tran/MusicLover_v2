using System.Collections.Generic;
using System.Threading.Tasks;
using MusicLover.WebApp.Server.Core.Models;

namespace MusicLover.WebApp.Server.Persistent.Repositories.Contracts
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetNewNotifications(string userId, bool isIncludeRelative = true);
        Task<IEnumerable<UserNotification>> GetNewUserNotifications(string userId);
    }
}