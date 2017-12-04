using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent;
using System.Threading.Tasks;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Core.Resources;

namespace MusicLover.WebApp.Server.Controllers.APIs
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GigsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGig([FromBody] SavedGigResource savedGigResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var gig = _mapper.Map<SavedGigResource, Gig>(savedGigResource);

            _context.GigSet.Add(gig);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<Gig, SavedGigResource>(gig);
            return Ok(result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGig(int id, [FromBody] SavedGigResource savedGigResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var gig = await _context.GigSet.SingleOrDefaultAsync(g => g.Id == id);
            if (gig == null)
                return NotFound(id);
            _mapper.Map<SavedGigResource, Gig>(savedGigResource, gig);

            await _context.SaveChangesAsync();

            gig = await _context.GigSet.SingleOrDefaultAsync(g=>g.Id == gig.Id);
            var result = _mapper.Map<Gig, SavedGigResource>(gig);
            return Ok(result);
        }

        [HttpDelete("/api/gigs/{:id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = User.GetUserId();
            var gig = await _context.GigSet
                              .Include(g => g.Genre)
                              .Include(g => g.Artist)
                                    .SingleOrDefaultAsync(g => g.Id == id);

            if (gig == null || gig.IsCancel)
                return NotFound(id + " not found");
            gig.Cancel();
            await _context.SaveChangesAsync();
            return Ok(gig);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGig(int id)
        {
            var gig = await _context.GigSet.SingleOrDefaultAsync(g => g.Id == id);
            if (gig == null)
                return NotFound(id);

            var gigResource = _mapper.Map<Gig, GigResource>(gig);
            return Ok(gigResource);
        }

        [HttpGet]
        public async Task<IEnumerable<GigResource>> GetGigs()
        {
            var gigs = await _context.GigSet.ToListAsync();

            return _mapper.Map<IEnumerable<Gig>, IEnumerable<GigResource>>(gigs);
        }



    }

}
