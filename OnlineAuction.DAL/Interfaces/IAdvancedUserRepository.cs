﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL.Interfaces
{
    public interface IAdvancedUserRepository : IRepository<AdvancedUser>
    {
        IEnumerable<AdvancedUser> Find(Func<AdvancedUser, Boolean> perdicate);
    }
}