using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL
{
    [Table("AdvancedUser")]
    public class AdvancedUser : Person
    {
        [Column("Admin", TypeName = "bit")]
        public bool Admin { get; set; } = false;
    }
}
