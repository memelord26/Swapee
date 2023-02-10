using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapee.Shared.Domain
{
    public class Payment : BaseDomainModel
    {
        public int Amount { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
