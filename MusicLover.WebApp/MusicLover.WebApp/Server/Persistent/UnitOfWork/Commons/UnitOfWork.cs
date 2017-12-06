using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicLover.WebApp.Server.Persistent.Repositories.Commons;
using MusicLover.WebApp.Server.Persistent.Repositories.Contracts;
using MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts;

namespace MusicLover.WebApp.Server.Persistent.UnitOfWork.Commons
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IAttendanceRepository _attendanceRepository;
        private IFollowingRepository _followingRepository;
        private IGenreRepository _genreRepository;
        private IGigRepository _gigRepository;
        private INotificationRepository _notificationRepository;
        private IPhotoRepository _photoRepository;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IAttendanceRepository AttendanceRepository => _attendanceRepository ?? (_attendanceRepository = new AttendanceRepository(_context));

        public IFollowingRepository FollowingRepository => _followingRepository ?? (_followingRepository = new FollowingRepository(_context));
        public IGenreRepository GenreRepository => _genreRepository ?? (_genreRepository = new GenreRepository(_context));
        public IGigRepository GigRepository => _gigRepository ?? (_gigRepository = new GigRepository(_context));
        public INotificationRepository NotificationRepository => _notificationRepository ?? (_notificationRepository = new NotificationRepository(_context));
        public IPhotoRepository PhotoRepository => _photoRepository ?? (_photoRepository= new PhotoRepository(_context));
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
