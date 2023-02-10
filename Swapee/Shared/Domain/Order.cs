using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapee.Shared.Domain
{
    public class Order : BaseDomainModel
    {
        public int Quantity { get; set; }
        public int BuyerId { get; set; }
        public virtual Buyer Buyer { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual List<Payment> Payments { get; set; }
    }
}