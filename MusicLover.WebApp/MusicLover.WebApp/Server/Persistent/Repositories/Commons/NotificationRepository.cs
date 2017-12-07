using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Core.Commons;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Persistent.Repositories.Contracts;

namespace MusicLover.WebApp.Server.Persistent.Repositories.Commons
{
    public class NotificationRepository: DataRepositoryBase<Notification>, INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Notification>> GetNewNotifications(string userId, bool isIncludeRelative = true)
        {
            if (isIncludeRelative)
            return await _context.UserNotificationSet
                .Where(u => u.UserId == userId && !u.IsRead)
                .Select(u => u.Notification)
                .Include(g => g.Gig.Artist)
                .ToListAsync();
            return await _context.UserNotificationSet
                .Where(u => u.UserId == userId && !u.IsRead)
                .Select(u=>u.Notification)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserNotification>> GetNewUserNotifications(string userId)
        {
            return await _context.UserNotificationSet
                .Where(u => u.UserId == userId && !u.IsRead)
                .ToListAsync();
        }
    }
}
