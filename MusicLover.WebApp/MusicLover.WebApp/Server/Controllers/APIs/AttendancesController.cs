using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Extensions;
using System.Threading.Tasks;
using MusicLover.WebApp.Server.Core.Resources;
using MusicLover.WebApp.Server.Persistent.Repositories.Commons;
using MusicLover.WebApp.Server.Persistent.Repositories.Contracts;
using MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts;

namespace MusicLover.WebApp.Server.Controllers.APIs
{
    [Route("/api/attendances/")]
    public class AttendancesController : Controller
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IAttendanceRepository attendanceRepository, IUnitOfWork unitOfWork)
        {
            _attendanceRepository = attendanceRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Attend([FromBody] int gigId)
        {
            var userId = User.GetUserId();
            var attendance = await _attendanceRepository.GetAttendance(gigId, userId);
            if (attendance != null) 
                return BadRequest(userId + " already attended " + gigId);

            attendance = new Attendance() { GigId = gigId, AttendeeId = userId };

            _attendanceRepository.Add(attendance);
            await _unitOfWork.CompleteAsync();
            return Ok(gigId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.GetUserId();

            var attendance =
                await _attendanceRepository.GetAttendance(id, userId);
            if (attendance == null)
                return NotFound(id);

            _attendanceRepository.Remove(attendance);
            await _unitOfWork.CompleteAsync();
            return Ok(id);
        }



    }
}
