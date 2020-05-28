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
    class ModerationRepository : IRepository<Moderation>,IDisposable
    {
        private OnlineAuctionContext db;

        public ModerationRepository(string connectionString)
        {
            this.db = new OnlineAuctionContext(connectionString);
        }
        public ModerationRepository(OnlineAuctionContext db)
        {
            this.db = db;
        }
        public void Create(Moderation item)
        {
            this.db.Moderation.Add(item);
        }

        public void Delete(int id)
        {
            Moderation moderation = db.Moderation.Find(id);
            if (moderation != null)
                db.Moderation.Remove(moderation);
        }

        public Moderation Get(int id)
        {
            return db.Moderation.Find(id);
        }

        public IQueryable<Moderation> GetAll()
        {
            return db.Moderation;
        }

        public void Update(Moderation item)
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
