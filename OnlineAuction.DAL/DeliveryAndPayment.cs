using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL
{
    [Table("DeliveryAndPayment")]
    public class DeliveryAndPayment
    {
        [Key, ForeignKey("Product")]
        public int Id { get; set; }
        [Column("DeliveryMethod", TypeName = "ntext")]
        [MaxLength(100)]
        [Required]
        public string DeliveryMethod { get; set; }
        [Column("SendingAbroad", TypeName = "ntext")]
        [MaxLength(50)]
        public string SendingAbroad { get; set; }
        [Column("PurchaseReturns", TypeName = "ntext")]
        [MaxLength(50)]
        public string PurchaseReturns { get; set; }
        [Column("CostOfdelivery", TypeName = "ntext")]
        [MaxLength(50)]
        public string CostOfdelivery { get; set; }
        public virtual Product Product { get; set; }
    }
}
