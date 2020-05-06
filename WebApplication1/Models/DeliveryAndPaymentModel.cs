using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class DeliveryAndPaymentModel
    {
        public int Id { get; set; }
        public string DeliveryMethod { get; set; }
        public string SendingAbroad { get; set; }
        public string PurchaseReturns { get; set; }
        public string CostOfdelivery { get; set; }
        public virtual ProductModel Product { get; set; }
    }
}
