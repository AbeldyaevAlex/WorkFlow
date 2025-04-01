using Asu.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Customization
{
    public partial class AmazonPlaceOrderResult
    {
        public bool IsSuccess { get; set; }
        public Order PlacedOrder { get; set; }
        public string ErrorMessage { get; set; }
    }
}
