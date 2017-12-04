using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Extensions;
using MusicLover.WebApp.Server.Persistent;
using System.Threading.Tasks;
using MusicLover.WebApp.Server.Core.Resources;
using MusicLover.WebApp.Server.Persistent.Repositories.Commons;
using MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts;

namespace MusicLover.WebApp.Server.Controllers.APIs
{
    [Route("/api/attendances/")]
    public class AttendancesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IMapper mapper, IAttendanceRepository attendanceRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _attendanceRepository = attendanceRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Attend([FromBody] AttendanceResource attendanceResource)
        {
            var userId = User.GetUserId();
            var gigId = attendanceResource.GigId;
            var existed = await _attendanceRepository.IsExisted(gigId, userId);
            if (existed)
                return BadRequest(userId + " already attended " + gigId);

            var attendance = new Attendance() { GigId = gigId, AttendeeId = userId };

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
