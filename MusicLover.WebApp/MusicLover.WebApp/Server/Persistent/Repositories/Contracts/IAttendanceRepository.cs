using System.Collections.Generic;
using System.Threading.Tasks;
using MusicLover.WebApp.Server.Core.Models;

namespace MusicLover.WebApp.Server.Persistent.Repositories.Contracts
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetFutureAttendances(string userId);
        Task<Attendance> GetAttendance(int gigId, string attendeeId);
        Task<bool> IsExisted(int gigId, string attendeeId);
        void Add(Attendance attendance);
        void Remove(Attendance attendance);
    }
}