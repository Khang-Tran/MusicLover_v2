using System.IO;
using System.Linq;

namespace MusicLover.WebApp.Server.Core.Models
{
    public class PhotoSettings
    {
        public int MaxByte { get; set; }
        public string[] AcceptedFileTypes { get; set; }

        public bool IsAccepted(string fileName)
        {
            return AcceptedFileTypes.All(s => s != Path.GetExtension(fileName).ToLower());
        }
    }
}
