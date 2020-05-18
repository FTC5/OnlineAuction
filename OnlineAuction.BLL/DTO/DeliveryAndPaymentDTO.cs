using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.DTO
{
    public class DeliveryAndPaymentDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Specify the delivery method")]
        public string DeliveryMethod { get; set; }
        public string SendingAbroad { get; set; }
        public string PurchaseReturns { get; set; }
        public string CostOfdelivery { get; set; }
    }
}
