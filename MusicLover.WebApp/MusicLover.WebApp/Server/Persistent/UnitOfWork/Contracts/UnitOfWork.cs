using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLover.WebApp.Server.Persistent.UnitOfWork.Contracts
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
