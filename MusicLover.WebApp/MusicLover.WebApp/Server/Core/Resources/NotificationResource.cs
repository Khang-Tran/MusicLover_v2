using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicLover.WebApp.Server.Core.Models;

namespace MusicLover.WebApp.Server.Core.Resources
{
    public class NotificationResource
    {
        public DateTime DateTime { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        public GigResource Gig { get; set; }
    }
}
