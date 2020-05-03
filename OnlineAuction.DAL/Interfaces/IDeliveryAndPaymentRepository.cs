using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL.Interfaces
{
    interface IDeliveryAndPaymentRepository
    {
        DeliveryAndPayment Get(int id);
        void Create(DeliveryAndPayment item);
        void Update(DeliveryAndPayment item);
        void Delete(int id);
    }
}
