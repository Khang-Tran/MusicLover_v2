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
                    new Genre() {Id = 1, Name = "Action"},
                    new Genre() {Id = 2, Name = "Fiction"},
                    new Genre() {Id = 3, Name = "Horror"},
                    new Genre() {Id = 4, Name = "Romantic"},
                    new Genre() {Id = 5, Name = "Advanture"}
                };

                _context.AddRange(genres);
                await _context.SaveChangesAsync();
            }
        }
    }
}
