using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.DTO
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public virtual ProductDTO Product { get; set; }
    }
}
