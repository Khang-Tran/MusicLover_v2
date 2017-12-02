using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicLover.WebApp.Server.Core.Models
{
    public class Photo
    {
        public int Id { get; set; }
        [StringLength(155)]
        public string FileName { get; set; }
        [ForeignKey("ApplicationUser")]
        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
