using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL
{
    [Table("Moderation")]
    public class Moderation
    {
        [Key,ForeignKey("Lot")]
        public int Id { get; set; }
        [Column("Comment", TypeName = "ntext")]
        [MaxLength(50)]
        public string Comment { get; set; }
        [Column("ModerationResult", TypeName = "bit")]
        [Required]
        public bool ModerationResult { get; set; } = false;
        Lot Lot { get; set; }
    }
}
