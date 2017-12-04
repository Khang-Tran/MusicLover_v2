using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent;
using System.Threading.Tasks;

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



    }

}
