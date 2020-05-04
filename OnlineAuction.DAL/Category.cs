using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL
{
    [Table("Category")]
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column("Name", TypeName = "ntext")]
        [MaxLength(50)]
        public string Name { get; set; }
        [ForeignKey("ParentCategory")]
        public int ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
    }
}
