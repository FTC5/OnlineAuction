using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.Web.Models
{
    public class AdvancedUserModel : PersonModel
    {
        public bool Admin { get; set; }
    }
}
