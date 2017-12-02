using System.ComponentModel.DataAnnotations.Schema;

namespace MusicLover.WebApp.Server.Core.Models
{
    public class Following
    {
        [ForeignKey("ApplicationUser")]
        public string FollowerId { get; set; }
        public ApplicationUser Follower { get; set; }
        [ForeignKey("ApplicationUser")]
        public string FolloweeId { get; set; }
        public ApplicationUser Followee { get; set; }

    }
}
