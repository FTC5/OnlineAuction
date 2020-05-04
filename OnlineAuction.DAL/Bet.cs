using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL
{
    [Table("Bet")]
    public class Bet
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public int Price { get; set; }
        public DateTime DateTime { get; set; }
        public virtual User User { get; set; }
        public virtual Lot Lot { get; set; }
    }
}
