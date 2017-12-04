using System.Collections.Generic;
using System.Threading.Tasks;
using MusicLover.WebApp.Server.Core.Models;

namespace MusicLover.WebApp.Server.Persistent.Repositories.Contracts
{
    public interface IGigRepository
    {
        Task<IEnumerable<Gig>> GetGigsUserAttending(string userId, bool isIncludeRelative = true);
        Task<Gig> GetGigWithAttendee(int gigId);
        Task<IEnumerable<Gig>> GetGigWithGenre(int gigId);
        Task<Gig> GetGigWithId(int gigId, bool isIncludeRelative = true);
        Task<IEnumerable<Gig>> GetAllUpcomingGigs(bool isIncludeRelative=true);
        Task<IEnumerable<Gig>> GetUpcomingGigsByArtist(string artistId, bool isIncludeRelative = true);
        void Add(Gig gig);
        void Remove(Gig gig);
    }
}