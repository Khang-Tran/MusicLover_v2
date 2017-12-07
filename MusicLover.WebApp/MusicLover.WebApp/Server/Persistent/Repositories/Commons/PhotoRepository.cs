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
    public class PhotoRepository : DataRepositoryBase<Photo>, IPhotoRepository
    {
        private readonly ApplicationDbContext _context;

        public PhotoRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }
        public async Task<Photo> GetPhoto(string userId)
        {
            return await _context.PhotoSet.SingleOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<ApplicationUser> GetUser(string userId)
        {
            return await _context.Users.SingleOrDefaultAsync(p => p.Id == userId);
        }
    }
}
