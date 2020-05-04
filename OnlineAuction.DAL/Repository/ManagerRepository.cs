using OnlineAuction.DAL.Context;
using OnlineAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            this.db.Manager.Add(item);
        }

        public void Delete(int id)
        {
            Manager manager = db.Manager.Find(id);
            if (manager != null)
                db.Manager.Remove(manager);
        }

        public Manager Get(int id)
        {
            return db.Manager.Find(id);
        }

        public void Update(Manager item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
