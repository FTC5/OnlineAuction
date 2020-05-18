using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.DTO
{
    public class ImageDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Provide a link to the image")]
        [Url(ErrorMessage = "Invalid hyperlink")]
        public string Link { get; set; }
    }
}
