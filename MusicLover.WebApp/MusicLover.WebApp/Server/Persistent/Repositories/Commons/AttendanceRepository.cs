using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicLover.WebApp.Server.Core.Models;

namespace MusicLover.WebApp.Server.Persistent.Repositories.Commons
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetFutureAttendances(string userId);
        Task<Attendance> GetAttendance(int gigId, string attendeeId);
        Task<bool> IsExisted(int gigId, string attendeeId);
        void Add(Attendance attendance);
        void Remove(Attendance attendance);
    }

    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Attendance>> GetFutureAttendances(string userId)
        {
            return await _context.AttendanceSet
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToListAsync();
        }

        public async Task<Attendance> GetAttendance(int gigId, string attendeeId)
        {
            return await _context.AttendanceSet
                .SingleOrDefaultAsync(a => a.AttendeeId == attendeeId && a.GigId == gigId);
        }

        public async Task<bool> IsExisted(int gigId, string attendeeId)
        {
            return await _context.AttendanceSet.AnyAsync(a => a.AttendeeId == attendeeId && a.GigId == gigId);
        }

        public void Add(Attendance attendance)
        {
            _context.AttendanceSet.Add(attendance);
        }

        public void Remove(Attendance attendance)
        {
            _context.AttendanceSet.Remove(attendance);
        }
    }
}
