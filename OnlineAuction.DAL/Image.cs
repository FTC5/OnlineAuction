using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL
{
    [Table("Image")]
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Column("Link", TypeName = "ntext")]
        [MaxLength(20)]
        [Required]
        public string Link { get; set; }
        public virtual Product Product { get; set; }
    }
}
