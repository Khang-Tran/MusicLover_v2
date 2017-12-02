using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent;
using System.Threading.Tasks;

namespace MusicLover.WebApp.Server.Controllers.APIs
{
    [Route("/api/attendances/")]
    public class AttendancesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AttendancesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Attend(int gigId, string attendeeId)
        {
            var userId = User.GetUserId();
            var existed = await _context.AttendanceSet.AnyAsync(a => a.AttendeeId == attendeeId & a.GigId == gigId);
            if (existed)
                return BadRequest(attendeeId + " already attended " + gigId);

            var attendance = new Attendance() { GigId = gigId, AttendeeId = userId };

            _context.AttendanceSet.Add(attendance);
            _context.SaveChanges();
            return Ok(gigId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.GetUserId();

            var attendance =
                await _context.AttendanceSet.SingleOrDefaultAsync(a => a.AttendeeId == userId && a.GigId == id);
            if (attendance == null)
                return NotFound(id);

            _context.AttendanceSet.Remove(attendance);
            _context.SaveChanges();
            return Ok(id);
        }



    }
}
