using Microsoft.AspNetCore.Mvc;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Extensions;
using System.Threading.Tasks;
using MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts;

namespace MusicLover.WebApp.Server.Controllers.APIs
{
    [Route("/api/attendances/")]
    public class AttendancesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Attend([FromBody] int gigId)
        {
            var userId = User.GetUserId();
            var existed = await _unitOfWork.AttendanceRepository.IsExisted(gigId, userId);
            if (existed) 
                return BadRequest(userId + " already attended " + gigId);

            var attendance = new Attendance() { GigId = gigId, AttendeeId = userId };

            _unitOfWork.AttendanceRepository.Add(attendance);
            await _unitOfWork.CompleteAsync();
            return Ok(gigId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.GetUserId();

            var attendance =
                await _unitOfWork.AttendanceRepository.GetAttendance(id, userId);
            if (attendance == null)
                return NotFound(id);

            _unitOfWork.AttendanceRepository.Remove(attendance);
            await _unitOfWork.CompleteAsync();
            return Ok(id);
        }



    }
}
