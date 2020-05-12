using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.Web.Models
{
    public class CategoryModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int ParentCategoryId { get; set; }
        public virtual CategoryModel ParentCategory { get; set; }
    }
}
