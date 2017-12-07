using System;
using System.Threading.Tasks;
using MusicLover.WebApp.Server.Persistent.Repositories.Contracts;

namespace MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts
{
    public interface IUnitOfWork: IDisposable
    {
        IAttendanceRepository AttendanceRepository { get; }
        IFolloweeRepository FolloweeRepository { get; }
        IFollowingRepository FollowingRepository { get; }
        IGenreRepository GenreRepository { get; }
        IGigRepository GigRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IPhotoRepository PhotoRepository { get; }
        Task CompleteAsync();
    }
}