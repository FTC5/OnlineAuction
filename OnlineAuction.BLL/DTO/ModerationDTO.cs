using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.DTO
{
    public class ModerationDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Write a comment, namely for what reason the lot was not accepted")]
        public string Comment { get; set; } = "";
        public bool ModerationResult { get; set; } = false;
        public virtual LotDTO Lot { get; set; }
    }
}
