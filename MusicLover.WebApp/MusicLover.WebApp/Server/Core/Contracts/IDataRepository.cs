﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLover.WebApp.Server.Core.Contracts
{
    public interface IDataRepository { }
    public interface IDataRepository<T>: IDataRepository
        where T: class, new()
    {
        void AddAsync(T entity);
        void Remove(T entity);
    }
}
