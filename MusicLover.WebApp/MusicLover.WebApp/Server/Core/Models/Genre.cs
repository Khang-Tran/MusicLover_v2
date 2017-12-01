using System.ComponentModel.DataAnnotations;

namespace MusicLover.WebApp.Server.Core.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [StringLength(155)]
        public string Name { get; set; }
    }
}
