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
    class AdvancedUserRepository : IRepository<AdvancedUser>,IAdvancedUserRepository,IDisposable
    {
        private OnlineAuctionContext db;

        public AdvancedUserRepository(string connectionString)
        {
            this.db = new OnlineAuctionContext(connectionString);
        }
        public AdvancedUserRepository(OnlineAuctionContext db)
        {
            this.db = db;
        }
        public void Create(AdvancedUser item)
        {
            this.db.AdvancedUser.Add(item);
        }

        public void Delete(int id)
        {
            AdvancedUser manager = db.AdvancedUser.Find(id);
            if (manager != null)
                db.AdvancedUser.Remove(manager);
        }

        public IQueryable<AdvancedUser> Find(Func<AdvancedUser, bool> perdicate)
        {
            return db.AdvancedUser.Where(perdicate).AsQueryable();
        }

        public AdvancedUser Get(int id)
        {
            return db.AdvancedUser.Find(id);
        }

        public IQueryable<AdvancedUser> GetAll()
        {
            return db.AdvancedUser;
        }

        public void Update(AdvancedUser item)
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
