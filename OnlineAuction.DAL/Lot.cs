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
        private ICollection<Image> image;
        public virtual ICollection<Image> Image
        {
            get { return image ?? (image = new List<Image>()); }
            set { image = value; }
        }
        [Key]
        public int Id { get; set; }
        public int MinimumStroke { get; set; } = 1;
        public int BetsCount { get; set; } = 0;
        [ForeignKey("Key")]
        public int UserId { get; set; }
        [Column("ModerationResult", TypeName = "bit")]
        public bool ModerationResult { get; set; } = false;
        [Required]
        public int TermDay { get; set; }
        public virtual User User { get; set; }

    }
}
