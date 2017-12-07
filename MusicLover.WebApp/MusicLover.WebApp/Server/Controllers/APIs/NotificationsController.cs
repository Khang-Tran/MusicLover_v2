using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicLover.WebApp.Server.Core.Resources;
using MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts;

namespace MusicLover.WebApp.Server.Controllers.APIs
{
    [Route("/api/notifications/")]
    public class NotificationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetNewNotifications()
        {
            var userId = User.GetUserId();
            var notifications = await _unitOfWork.NotificationRepository.GetNewNotifications(userId);
            return Ok(_mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationResource>>(notifications));
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead()
        {
            var userId = User.GetUserId();
            var notifications = await _unitOfWork.NotificationRepository.GetNewUserNotifications(userId);
            notifications.ToList().ForEach(n => n.Read());
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
    }
}
