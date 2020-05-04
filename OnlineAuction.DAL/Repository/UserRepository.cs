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
    class UserRepository : IRepository<User>
    {
        private OnlineAuctionContext db;

        public UserRepository(OnlineAuctionContext db)
        {
            this.db = db;
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
    }
}
