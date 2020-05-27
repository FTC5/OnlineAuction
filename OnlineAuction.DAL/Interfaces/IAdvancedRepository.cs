﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL.Interfaces
{
    public interface IAdvancedRepositor<T> : IRepository<T> where T : class
    {
        IEnumerable<T> Find(Func<T, bool> perdicate);
    }
}