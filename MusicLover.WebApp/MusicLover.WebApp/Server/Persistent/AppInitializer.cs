using MusicLover.WebApp.Server.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLover.WebApp.Server.Persistent
{
    public class AppInitializer
    {
        private readonly ApplicationDbContext _context;

        public AppInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (!_context.GenreSet.Any())
            {
                var genres = new List<Genre>()
                {
                    new Genre() {Name = "Action"},
                    new Genre() {Name = "Fiction"},
                    new Genre() {Name = "Horror"},
                    new Genre() {Name = "Romantic"},
                    new Genre() {Name = "Advanture"}
                };

                _context.AddRange(genres);
                await _context.SaveChangesAsync();
            }
        }
    }
}
