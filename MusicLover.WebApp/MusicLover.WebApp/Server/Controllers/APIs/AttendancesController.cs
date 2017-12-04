using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent;
using System.Threading.Tasks;
using MusicLover.WebApp.Server.Core.Resources;

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
        public async Task<IActionResult> Attend([FromBody] AttendanceResource attendanceResource)
        {
            var userId = User.GetUserId();
            var gigId = attendanceResource.GigId;
            var existed = await _context.AttendanceSet
                .AnyAsync(a => a.AttendeeId == userId & a.GigId == gigId);
            if (existed)
                return BadRequest(userId + " already attended " + gigId);

            var attendance = new Attendance() { GigId = gigId, AttendeeId = userId };

            _context.AttendanceSet.Add(attendance);
            await _context.SaveChangesAsync();
            return Ok(gigId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.GetUserId();

            var attendance =
                await _context.AttendanceSet
                .SingleOrDefaultAsync(a => a.AttendeeId == userId && a.GigId == id);
            if (attendance == null)
                return NotFound(id);

            _context.AttendanceSet.Remove(attendance);
            await _context.SaveChangesAsync();
            return Ok(id);
        }



    }
}
