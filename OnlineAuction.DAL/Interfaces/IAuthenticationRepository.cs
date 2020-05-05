﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL.Interfaces
{
    public interface IAuthenticationRepository : IRepository<Authentication>
    {
        IEnumerable<Authentication> Find(Func<Authentication, Boolean> perdicate);
    }
}