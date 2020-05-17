using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.DTO
{
    public class AuthenticationDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Specify a login")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The login must have at least three characters")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Enter the password")]
        [StringLength(50, MinimumLength = 9, ErrorMessage = "The password must be at least 9 characters long")]
        public string Password { get; set; }
    }
}
