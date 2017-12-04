using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MusicLover.WebApp.Server.Core.Models
{
    public class Gig
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [ForeignKey("ApplicationUser")]
        public string ArtistId { get; set; }
        public ApplicationUser Artist { get; set; }


        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        [Required]
        [ForeignKey("Genre")]

        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        public bool IsCancel { get; set; }

        public ICollection<Attendance> Attendances { get; set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCancel = true;
            var notification = Notification.GigCanceled(this);

            var attendees = Attendances.Select(a => a.Attendee);
            foreach (var attendee in attendees)
            {
                attendee.Notify(notification);
            }
        }

    }
}
