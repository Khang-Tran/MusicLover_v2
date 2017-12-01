using System.ComponentModel.DataAnnotations;

namespace MusicLover.WebApp.Server.Core.Models
{
    public class Photo
    {
        public int Id { get; set; }
        [StringLength(155)]
        public string FileName { get; set; }
        public int UserId { get; set; }
    }
}
