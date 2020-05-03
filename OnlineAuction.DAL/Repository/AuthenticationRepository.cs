using OnlineAuction.DAL.Context;
using OnlineAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Authentication Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Authentication item)
        {
            throw new NotImplementedException();
        }
    }
}
