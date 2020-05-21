using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.DTO
{
    public class LotDTO
    {
        private ICollection<BetDTO> bets;
        public virtual ICollection<BetDTO> Bets
        {
            get { return bets; }
            set { bets = value; }
        }
        public int Id { get; set; }
        public int CurrentPrice { get; set; }
        [Required(ErrorMessage = "Specify a minimum bid")]
        public int MinimumStroke { get; set; }
        public int BetsCount { get; set; } = 0;
        public int UserId { get; set; }
        public bool ModerationResult { get; set; } = false;
        public bool Change { get; set; } = false;
        public bool Sels { get; set; } = false;
        public DateTime StartDate { get; set; }
        public int TermDay { get; set; }
        public string UserName { get; set; }
        public virtual UserDTO User { get; set; }
        [Required(ErrorMessage = "Lot dont have Product")]
        public virtual ProductDTO Product { get; set; }
        public virtual ModerationDTO Moderation { get; set; }
    }
}
