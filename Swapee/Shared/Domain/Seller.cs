﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapee.Shared.Domain
{
    public class Seller : BaseDomainModel
    {
        public string Name { get; set; }
        public int Contact { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}