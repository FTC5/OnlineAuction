using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class AdvancedUserModel : PersonModel
    {
        public bool Admin { get; set; }
    }
}
