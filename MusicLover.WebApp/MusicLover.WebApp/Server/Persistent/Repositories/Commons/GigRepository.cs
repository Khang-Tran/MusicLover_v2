using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Core.Commons;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Persistent.Repositories.Contracts;

namespace MusicLover.WebApp.Server.Persistent.Repositories.Commons
{
    public class GigRepository : DataRepositoryBase<Gig>, IGigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Gig>> GetGigsUserAttending(string userId, bool isIncludeRelative = true)
        {
            if (isIncludeRelative)
            return await _context.AttendanceSet
                .Where(a => a.AttendeeId == userId)
                .Select(g => g.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToListAsync();

            return await _context.AttendanceSet.Where(a => a.AttendeeId == userId)
                .Select(g => g.Gig)
                .ToListAsync();
        }

        public async Task<Gig> GetGigWithAttendee(int gigId)
        {
            return await _context.GigSet
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefaultAsync(g => g.Id == gigId);
        }

        public async Task<IEnumerable<Gig>> GetGigWithGenre(int gigId)
        {
            return await _context.GigSet.Include(g => g.Genre).ToListAsync();
        }

        public async Task<Gig> GetGigWithId(int gigId, bool isIncludeRelative = true)
        {
            if (isIncludeRelative)
            return await _context.GigSet
                .Include(g => g.Genre)
                .Include(g => g.Artist)
                .SingleOrDefaultAsync(g => g.Id == gigId);
             return await _context.GigSet
                .SingleOrDefaultAsync(g => g.Id == gigId);
        }

        public async Task<IEnumerable<Gig>> GetAllUpcomingGigs(bool isIncludeRelative=true)
        {
            if (isIncludeRelative)
            return await _context.GigSet
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCancel)
                .ToListAsync();
            return await _context.GigSet.ToListAsync();
        }


        public async Task<IEnumerable<Gig>> GetUpcomingGigsByArtist(string artistId, bool isIncludeRelative = true)
        {
            if (isIncludeRelative)
            return await _context.GigSet
                .Where(g => g.ArtistId == artistId && g.DateTime > DateTime.Now && !g.IsCancel)
                .Include(g => g.Genre)
                .Include(g => g.Artist)
                .ToListAsync();
            return await _context.GigSet
                .Where(g => g.ArtistId == artistId && g.DateTime > DateTime.Now && !g.IsCancel)
                .ToListAsync(); 
        }

    }
}
