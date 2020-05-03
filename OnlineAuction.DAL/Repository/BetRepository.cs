using OnlineAuction.DAL.Context;
using OnlineAuction.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL.Repository
{
    public class BetRepository : IRepository<Bet>, IBetRepository
    {
        private OnlineAuctionContext db;

        public BetRepository(OnlineAuctionContext db)
        {
            this.db = db;
        }

        public void Create(Bet item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Bet Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Bet> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Bet item)
        {
            throw new NotImplementedException();
        }
        private bool disposed = false;
    }
}
