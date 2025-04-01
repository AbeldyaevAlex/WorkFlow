using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Customization
{
    public partial class AmazonPaymentAdvancedTask : BaseEntity
    {
        public bool IsBusy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
