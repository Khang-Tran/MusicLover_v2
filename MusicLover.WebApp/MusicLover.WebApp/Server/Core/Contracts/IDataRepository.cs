using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLover.WebApp.Server.Core.Contracts
{
    public interface IDataRepository<T>
        where T: class, new()
    {
        void Add(T entity);
        void Remove(T entity);
    }
}
