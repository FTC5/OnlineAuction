using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter a name for the category")]
        public string Name { get; set; }
        [Required]
        public int ParentCategoryId { get; set; }
        public virtual CategoryDTO ParentCategory { get; set; }
    }
}
