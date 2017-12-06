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
    public class FollowingRepository : DataRepositoryBase<Following>, IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public async Task<Following> GetFollowing(string artistId, string followerId)
        {
            return await _context.FollowingSet
                .SingleOrDefaultAsync(a => a.FolloweeId == artistId && a.FollowerId == followerId);
        }

        public async Task<bool> IsExist(string followerId, string followeeId)
        {
            return await _context.FollowingSet
                .AnyAsync(f => f.FollowerId == followerId && f.FolloweeId == followeeId);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllFollowee(string followerId)
        {
            return await _context.FollowingSet
                .Where(f => f.FollowerId == followerId)
                .Select(g => g.Followee)
                .ToListAsync();
        }
    }
}
