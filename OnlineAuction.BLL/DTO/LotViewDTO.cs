using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.DTO
{
    public class LotViewDTO
    {
        int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public int BetsCount { get; set; } = 0;
        public int CurrentPrice { get; set; }
        public string FirstImageUrl { get; set; }
        public bool ModerationResult { get; set; } = false;
        public bool Change { get; set; } = true;
        public bool Sels { get; set; } = false;
        public virtual ModerationDTO Moderation { get; set; }
        public virtual ImageDTO Image { get; set; }
        public virtual CategoryDTO Category { get; set; }
    }
}
