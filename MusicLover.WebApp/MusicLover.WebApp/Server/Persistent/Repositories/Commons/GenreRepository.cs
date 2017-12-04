using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Persistent.Repositories.Contracts;

namespace MusicLover.WebApp.Server.Persistent.Repositories.Commons
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.GenreSet.ToListAsync();
        }
    }
}
