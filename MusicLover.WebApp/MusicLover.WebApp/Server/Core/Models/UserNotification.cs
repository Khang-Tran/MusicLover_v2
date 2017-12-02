using System.ComponentModel.DataAnnotations.Schema;

namespace MusicLover.WebApp.Server.Core.Models
{
    public class UserNotification
    {
        [ForeignKey("ApplicationUser")]

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [ForeignKey("Notification")]

        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
        public bool IsRead { get; set; }
    }
}
