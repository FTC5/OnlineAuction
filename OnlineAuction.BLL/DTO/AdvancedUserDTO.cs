﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.BLL.DTO
{
    public class AdvancedUserDTO : PersonDTO
    {
        public bool Admin { get; set; }
    }
}
