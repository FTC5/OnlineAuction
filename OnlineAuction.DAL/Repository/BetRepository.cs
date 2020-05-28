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
    public class BetRepository : IRepository<Bet>,IDisposable
    {
        private OnlineAuctionContext db;

        public BetRepository(string connectionString)
        {
            this.db = new OnlineAuctionContext(connectionString);
        }
        public BetRepository(OnlineAuctionContext db)
        {
            this.db = db;
        }
        public void Create(Bet item)
        {
            this.db.Bet.Add(item);
        }

        public void Delete(int id)
        {
            Bet bet = db.Bet.Find(id);
            if (bet != null)
                db.Bet.Remove(bet);
        }

        public Bet Get(int id)
        {
            return db.Bet.Find(id);
        }

        public IQueryable<Bet> GetAll()
        {
            return db.Bet;
        }

        public void Update(Bet item)
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
