﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuction.Web.Models
{
    public class ModerationModel
    {
        public int? Id { get; set; }
        public string Comment { get; set; } = "";
        public bool ModerationResult { get; set; } = false;
    }
}
