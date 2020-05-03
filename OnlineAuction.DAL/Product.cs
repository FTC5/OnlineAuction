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
        public Category Category { get; set; }
    }
}
