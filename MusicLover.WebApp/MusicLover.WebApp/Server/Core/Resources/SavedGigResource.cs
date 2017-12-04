using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLover.WebApp.Server.Core.Resources
{
    public class SavedGigResource
    {
        public int Id { get; set; }
        public string Venue { get; set; }
   
        public string Date { get; set; }
      
        public string Time { get; set; }
       
        public int GenreId { get; set; }
       
    }
}
