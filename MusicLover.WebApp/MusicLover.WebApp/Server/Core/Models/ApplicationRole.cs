using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MusicLover.WebApp.Server.Core.Models
{
    public class ApplicationRole : IdentityRole<string>
    {
        [StringLength(255)]
        public string Description { get; set; }
    }
}
