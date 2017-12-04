using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLover.WebApp.Server.Core.Resources
{
    public class GigResource
    {
        public int Id { get; set; }
        public UserResource Artist { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public GenreResource Genre { get; set; }
    }
}
