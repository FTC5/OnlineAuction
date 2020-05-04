using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL
{
    [Table("Authentication")]
    public class Authentication
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("Login", TypeName = "ntext")]
        [MaxLength(50)]
        public string Login { get; set; }
        [Column("Password", TypeName = "ntext")]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
