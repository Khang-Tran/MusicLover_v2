using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLover.WebApp.Server.Controllers.APIs
{
    [Route("/api/notifications/")]
    public class NotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public NotificationsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Notification>> GetNewNotifications()
        {
            var userId = User.GetUserId();
            var notifications = await _context.UserNotificationSet.Where(u => u.UserId == userId && !u.IsRead)
                .Select(u => u.Notification)
                .Include(g => g.Gig.Artist)
                .ToListAsync();
            return notifications;
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead()
        {
            var userId = User.GetUserId();
            var notifications = await _context.UserNotificationSet.Where(u => u.UserId == userId && !u.IsRead)
                .ToListAsync();
            notifications.ForEach(n => n.Read());
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
