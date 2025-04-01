using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Catalog
{
    public class BestSellerProduct : BaseEntity
    {
        public int ProductId { get; set; }

        public int TotalQty { get; set; }

        public DateTime? LastOrderDate { get; set; }
    }
}
