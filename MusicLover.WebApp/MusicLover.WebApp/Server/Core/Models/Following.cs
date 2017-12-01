namespace MusicLover.WebApp.Server.Core.Models
{
    public class Following
    {
        public string FollowerId { get; set; }
        public ApplicationUser Follower { get; set; }
        public ApplicationUser Followee { get; set; }
        public string FolloweeId { get; set; }
    }
}
