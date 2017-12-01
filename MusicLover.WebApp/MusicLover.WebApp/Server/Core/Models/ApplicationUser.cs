using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicLover.WebApp.Server.Core.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        [StringLength(155)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(155)]
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public Photo ProfilePhoto { get; set; }

        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }

        public ApplicationUser()
        {
            Followers = new Collection<Following>();
            Followees = new Collection<Following>();
            UserNotifications = new Collection<UserNotification>();
        }

        [NotMapped]
        public string Name => FirstName + " " + LastName;
    }
}
