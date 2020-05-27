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
    class AuthenticationRepository : IRepository<Authentication>, IAdvancedRepositor<Authentication>, IDisposable
    {
        private OnlineAuctionContext db;

        public AuthenticationRepository(string connectionString)
        {
            this.db = new OnlineAuctionContext(connectionString);
        }

        public void Create(Authentication item)
        {
            this.db.Authentication.Add(item);
        }

        public void Delete(int id)
        {
            Authentication aut = db.Authentication.Find(id);
            if (aut != null)
                db.Authentication.Remove(aut);
        }

        public IEnumerable<Authentication> Find(Func<Authentication, bool> perdicate)
        {
            return db.Authentication.Where(perdicate).ToList();
        }

        public Authentication Get(int id)
        {
            return db.Authentication.Find(id);
        }

        public IQueryable<Authentication> GetAll()
        {
            return db.Authentication;
        }

        public void Update(Authentication item)
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
