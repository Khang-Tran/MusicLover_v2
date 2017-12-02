using System.ComponentModel.DataAnnotations.Schema;

namespace MusicLover.WebApp.Server.Core.Models
{
    public class Attendance
    {
        [ForeignKey("Attendee")]
        public string AttendeeId { get; set; }
        public ApplicationUser Attendee { get; set; }
        [ForeignKey("Gig")]
        public int GigId { get; set; }
        public Gig Gig { get; set; }
    }
}
