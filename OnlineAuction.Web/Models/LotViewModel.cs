using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineAuction.Web.Models
{
    public class LotViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public int BetsCount { get; set; } = 0;
        public int CurrentPrice { get; set; }
        public bool ModerationResult { get; set; } = false;
        public bool Change { get; set; } = true;
        public bool Sels { get; set; } = false;
        public virtual ModerationModel Moderation { get; set; }
        public virtual ImageModel Image { get; set; }
        public virtual CategoryModel Category { get; set; }
    }
}