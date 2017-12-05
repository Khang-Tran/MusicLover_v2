using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLover.WebApp.Server.Core.Resources
{
    public class SavedGigResource
    {
        public string ArtistId { get; set; }
        
        public string Venue { get; set; }

        public DateTime DateTime;
       
        public int GenreId { get; set; }
       
    }
}
