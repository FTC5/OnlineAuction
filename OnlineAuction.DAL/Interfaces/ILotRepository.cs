using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL.Interfaces
{
    public interface ILotRepository : IRepository<Lot>
    {
        IEnumerable<Lot> Find(Func<Lot, Boolean> perdicate);
    }
}
