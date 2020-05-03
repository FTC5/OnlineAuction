using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineAuction.DAL.Context;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.DAL.Repository
{
    class DeliveryAndPaymentRepository : IRepository<DeliveryAndPayment>
    {
        private OnlineAuctionContext db;

        public DeliveryAndPaymentRepository(OnlineAuctionContext db)
        {
            this.db = db;
        }

        public void Create(DeliveryAndPayment item)
        {
            this.db.DeliveryAndPayment.Add(item);
        }

        public void Delete(int id)
        {
            DeliveryAndPayment deliveryAndPayment = db.DeliveryAndPayment.Find(id);
            if (deliveryAndPayment != null)
                db.DeliveryAndPayment.Remove(deliveryAndPayment);
        }

        public DeliveryAndPayment Get(int id)
        {
            return db.DeliveryAndPayment.Find(id);
        }

        public IQueryable<DeliveryAndPayment> GetAll()
        {
            return db.DeliveryAndPayment;
        }

        public void Update(DeliveryAndPayment item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
