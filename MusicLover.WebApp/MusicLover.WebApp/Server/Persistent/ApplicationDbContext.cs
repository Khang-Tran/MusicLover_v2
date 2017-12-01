using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Core.Models;

namespace MusicLover.WebApp.Server.Persistent
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DbSet<Gig> GigSet { get; set; }
        public DbSet<Genre> GenreSet { get; set; }
        public DbSet<Attendance> AttendanceSet { get; set; }
        public DbSet<Following> FollowingSet { get; set; }
        public DbSet<Notification> NotificationSet { get; set; }
        public DbSet<UserNotification> UserNotificationSet { get; set; }
        public DbSet<ApplicationUser> UserSet { get; set; }
        public DbSet<ApplicationRole> UserRoleSet { get; set; }
        public DbSet<Photo> PhotoSet { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Attendance>().HasKey(g => new { g.GigId, g.AttendeeId });
            builder.Entity<Following>().HasKey(g => new { g.FolloweeId, g.FollowerId });
            builder.Entity<Gig>().HasMany(g => g.Attendances).WithOne(a => a.Gig).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ApplicationUser>().HasMany(f => f.Followers).WithOne(f => f.Followee)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ApplicationUser>().HasMany(f => f.Followers).WithOne(f => f.Followee)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<UserNotification>().HasKey(g => new { g.UserId, g.NotificationId });
            builder.Entity<UserNotification>().HasOne(u => u.User).WithMany(u => u.UserNotifications)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(builder);
        }
    }
}
