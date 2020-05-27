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
    class LotReposytory : IRepository<Lot>, IAdvancedRepositor<Lot>, IDisposable
    {
        private OnlineAuctionContext db;

        public LotReposytory(string connectionString)
        {
            this.db = new OnlineAuctionContext(connectionString);
        }

        public void Create(Lot item)
        {
            this.db.Lot.Add(item);
        }

        public void Delete(int id)
        {
            Lot lot = db.Lot.Find(id);
            if (lot != null)
                db.Lot.Remove(lot);
        }

        public IEnumerable<Lot> Find(Func<Lot, bool> perdicate)
        {
            return db.Lot.Where(perdicate).ToList();
        }

        public Lot Get(int id)
        {
            return db.Lot.Find(id);
        }

        public IQueryable<Lot> GetAll()
        {
            return db.Lot;
        }

        public void Update(Lot item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        private bool disposed = false;

        internal virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
