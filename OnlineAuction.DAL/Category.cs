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
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public string Name { get; set; }
        [ForeignKey("ParentCategory")]
        public string ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
    }
}
