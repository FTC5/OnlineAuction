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
    class ProductReposytory : IRepository<Product>,IDisposable
    {
        private OnlineAuctionContext db;

        public ProductReposytory(string connectionString)
        {
            this.db = new OnlineAuctionContext(connectionString);
        }

        public void Create(Product item)
        {
            this.db.Product.Add(item);
        }

        public void Delete(int id)
        {
            Product product = db.Product.Find(id);
            if (product != null)
                db.Product.Remove(product);
        }

        public Product Get(int id)
        {
            return db.Product.Find(id);
        }

        public IQueryable<Product> GetAll()
        {
            return db.Product;
        }

        public void Update(Product item)
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
