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
    class CategoryReposytory : IRepository<Category>,IAdvancedRepositor<Category>,IDisposable
    {
        private OnlineAuctionContext db;

        public CategoryReposytory(string connectionString)
        {
            this.db = new OnlineAuctionContext(connectionString);
        }

        public void Create(Category item)
        {
            this.db.Category.Add(item);
        }

        public void Delete(int id)
        {
            Category category = db.Category.Find(id);
            if (category != null)
                db.Category.Remove(category);
        }

        public IEnumerable<Category> Find(Func<Category, bool> perdicate)
        {
            return db.Category.Where(perdicate).ToList();
        }

        public Category Get(int id)
        {
            return db.Category.Find(id);
        }

        public IQueryable<Category> GetAll()
        {
            return db.Category;
        }

        public void Update(Category item)
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
