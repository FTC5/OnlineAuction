﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public virtual ProductModel Product { get; set; }
    }
}
