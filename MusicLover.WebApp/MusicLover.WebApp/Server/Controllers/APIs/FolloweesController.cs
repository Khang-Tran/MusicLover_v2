using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Core.Resources;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent;

namespace MusicLover.WebApp.Server.Controllers.APIs
{
    [Route("/api/followees/")]
    public class FolloweesController:Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FolloweesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserResource>> GetFollowees()
        {
            var userId = User.GetUserId();

            var followees =await _context.FollowingSet.Where(f => f.FolloweeId == userId)
                .Select(g => g.Followee).ToListAsync();
            return _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserResource>>(followees);
        }
    }
}
