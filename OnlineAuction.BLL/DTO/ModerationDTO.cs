using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace OnlineAuction.BLL.DTO
{
    public class ModerationDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; } = "";
        public bool ModerationResult { get; set; } = false;
    }
}
