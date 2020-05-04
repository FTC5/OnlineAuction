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
    class AuthenticationRepository : IRepository<Authentication>
    {
        private OnlineAuctionContext db;

        public AuthenticationRepository(OnlineAuctionContext db)
        {
            this.db = db;
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

        public Authentication Get(int id)
        {
            return db.Authentication.Find(id);
        }

        public void Update(Authentication item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
