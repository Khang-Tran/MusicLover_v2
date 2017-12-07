using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MusicLover.WebApp.Server.Core.Contracts;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Persistent;

namespace MusicLover.WebApp.Server.Core.Commons
{
    public class DataRepositoryBase<T>: IDataRepository<T>
        where T:class, new()
    {
        private readonly ApplicationDbContext _context;

        protected DataRepositoryBase(ApplicationDbContext context)
        {
            _context = context;
        }
        public async void AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public void Remove(T entity)
        {
            _context.Remove(entity);
        }
    }
}
