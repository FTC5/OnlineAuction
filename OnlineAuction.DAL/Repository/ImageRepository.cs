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
    class ImageRepository : IRepository<Image>,IDisposable
    {
        private OnlineAuctionContext db;

        public ImageRepository(string connectionString)
        {
            this.db = new OnlineAuctionContext(connectionString);
        }

        public void Create(Image item)
        {
            this.db.Image.Add(item);
        }

        public void Delete(int id)
        {
            Image image = db.Image.Find(id);
            if (image != null)
                db.Image.Remove(image);
        }

        public Image Get(int id)
        {
            return db.Image.Find(id);
        }

        public IQueryable<Image> GetAll()
        {
            return db.Image;
        }

        public void Update(Image item)
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
