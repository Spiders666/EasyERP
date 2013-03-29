﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Type> Types { get; set; }
    }
}