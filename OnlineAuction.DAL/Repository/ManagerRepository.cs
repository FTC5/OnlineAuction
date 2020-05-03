using OnlineAuction.DAL.Context;
using OnlineAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL.Repository
{
    class ManagerRepository : IRepository<Manager>
    {
        private OnlineAuctionContext db;

        public ManagerRepository(OnlineAuctionContext db)
        {
            this.db = db;
        }

        public void Create(Manager item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Manager Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Manager item)
        {
            throw new NotImplementedException();
        }
    }
}
