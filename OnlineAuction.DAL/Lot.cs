using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.DAL
{
    [Table("Lot")]
    public class Lot
    {
        private ICollection<Bet> bets;
        public virtual ICollection<Bet> Bets
        {
            get { return bets ?? (bets = new List<Bet>()); }
            set { bets = value; }
        }
        [Key,ForeignKey("Product")]
        public int Id { get; set; }
        public int CurrentPrice { get; set; }
        public int MinimumStroke { get; set; } = 1;
        public int BetsCount { get; set; } = 0;
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Column("ModerationResult", TypeName = "bit")]
        public bool ModerationResult { get; set; } = false;
        public bool Change { get; set; } = true; 
        [Required]
        [Column("StartDate", TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Required]
        public int TermDay { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
        public virtual Moderation Moderation { get; set; }

    }
}
