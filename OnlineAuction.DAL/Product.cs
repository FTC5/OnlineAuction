using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL
{
    [Table("Product")]
    public class Product
    {
        private ICollection<Image> image;
        public virtual ICollection<Image> Images
        {
            get { return image ?? (image = new List<Image>()); }
            set { image = value; }
        }
        [Key]
        public int Id { get; set; }
        [Column("Name", TypeName = "ntext")]
        [MaxLength(60)]
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Column("Description", TypeName = "ntext")]
        [MaxLength(50)]
        [Required]
        public string Description { get; set; }
        [Column("Location", TypeName = "ntext")]
        [MaxLength(50)]
        [Required]
        public string Location { get; set; }
        [ForeignKey("Category")]
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual DeliveryAndPayment DeliveryAndPayment { get; set; }
        public virtual Lot Lot { get; set; }
    }
}
