using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapee.Shared.Domain
{
    public class Product : BaseDomainModel
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int SellerId { get; set; }
        public virtual Seller Seller { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
