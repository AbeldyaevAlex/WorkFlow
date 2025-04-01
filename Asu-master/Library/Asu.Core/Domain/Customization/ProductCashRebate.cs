using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Customization
{
    public partial class ProductCashRebate : BaseEntity
    {
        public int ProductId { get; set; }
        public decimal RebateAmount { get; set; }
    }
}
