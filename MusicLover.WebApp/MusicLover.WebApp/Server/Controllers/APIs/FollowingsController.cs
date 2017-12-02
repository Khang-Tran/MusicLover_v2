using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent;
using System.Threading.Tasks;

namespace MusicLover.WebApp.Server.Controllers.APIs
{
    [Route("/api/followings/")]
    public class FollowingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FollowingsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Follow(string followeeId)
        {
            var userId = User.GetUserId();
            var existed = await _context.FollowingSet.AnyAsync(f => f.FolloweeId == followeeId && f.FollowerId == userId);
            if (existed)
                return BadRequest(followeeId + "existed");
            var follow = new Following()
            {
                FolloweeId = followeeId,
                FollowerId = userId
            };
            _context.FollowingSet.Add(follow);
            await _context.SaveChangesAsync();
            return Ok(followeeId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = User.GetUserId();
            var following =
                await _context.FollowingSet.SingleOrDefaultAsync(a => a.FolloweeId == id && a.FollowerId == userId);
            if (following == null)
                return NotFound(id);
            _context.FollowingSet.Remove(following);
            await _context.SaveChangesAsync();
            return Ok(id);
        }
    }
}
