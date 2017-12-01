namespace MusicLover.WebApp.Server.Core.Models
{
    public class UserNotification
    {
        public string UserId { get; private set; }
        public ApplicationUser User { get; private set; }

        public int NotificationId { get; private set; }
        public Notification Notification { get; private set; }
        public bool IsRead { get; private set; }
    }
}
