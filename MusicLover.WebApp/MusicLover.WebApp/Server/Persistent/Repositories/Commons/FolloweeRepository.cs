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
    public class FolloweeRepository: DataRepositoryBase<ApplicationUser>, IFolloweeRepository
    {
        private readonly ApplicationDbContext _context;

        public FolloweeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllFollowees(string userId)
        {
            return await _context.FollowingSet.Where(f => f.FolloweeId == userId)
                .Select(g => g.Followee).ToListAsync();
        }
    }
}
