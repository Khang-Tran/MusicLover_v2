using System;
using System.ComponentModel.DataAnnotations;

namespace MusicLover.WebApp.Server.Core.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        [Required]
        public Gig Gig { get; set; }


        public Notification()
        {

        }

        private Notification(Gig gig, NotificationType notificationType)
        {
            Gig = gig ?? throw new Exception("Gig is null");
            NotificationType = notificationType;
            DateTime = DateTime.Now; ;
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCreated);
        }

        public static Notification GigUpdated(Gig gig, DateTime originalDate, string originalVenue)
        {
            return new Notification(gig, NotificationType.GigUpdated)
            {
                OriginalDateTime = originalDate,
                OriginalVenue = originalVenue
            };
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCanceled);
        }


    }
}
