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
    class UserRepository : IRepository<User>,IDisposable
    {
        private OnlineAuctionContext db;

        public UserRepository(string connectionString)
        {
            this.db = new OnlineAuctionContext(connectionString);
        }

        public void Create(User item)
        {
            this.db.User.Add(item);
        }

        public void Delete(int id)
        {
            User user = db.User.Find(id);
            if (user != null)
                db.User.Remove(user);
        }

        public User Get(int id)
        {
            return db.User.Find(id);
        }

        public IQueryable<User> GetAll()
        {
            return db.User;
        }

        public void Update(User item)
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
