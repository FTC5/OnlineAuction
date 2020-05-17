using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.DTO
{
    public class PersonDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter your name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter your last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Specify the date of birth")]
        public string DateOfBirth { get; set; }
        [Required(ErrorMessage = "Enter a phone number")]
        [RegularExpression(@"([+]?\d{1,}[(]\d{3}[)]\d{3}[-\s]?\d{2}[-\s]?\d{2})|([(]?\d{3}[)]?[-\s]?\d{3}([-\s]?\d{2}){2})"
            , ErrorMessage = "The phone number is in the wrong format")]
        //[Phone]
        public int PhoneNumber { get; set; }
        public virtual AuthenticationDTO Authentication { get; set; }
    }
}
