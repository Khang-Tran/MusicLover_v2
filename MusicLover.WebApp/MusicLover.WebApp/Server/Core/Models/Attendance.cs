namespace MusicLover.WebApp.Server.Core.Models
{
    public class Attendance
    {
        public string AttendeeId { get; set; }
        public ApplicationUser Attendee { get; set; }
        public int GigId { get; set; }
        public Gig Gig { get; set; }
    }
}
