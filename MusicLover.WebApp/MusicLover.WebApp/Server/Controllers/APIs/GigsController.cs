using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Persistent;

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
        /*
        [HttpDelete("/api/gigs/{:id}")]

        public async IActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = await _context.GigSet
                              .Include(g => g.Genre)
                              .Include(g => g.Artist)
                                    .SingleOrDefaultAsync(g => g.Id == id);

            if (gig == null || gig.IsCancel)
                return NotFound(id + " not found");

            if (gig.ArtistId != userId)
                return Unauthorized("Access denied");

            gig.IsCancel = true;
    

        } */


       
    }

}
