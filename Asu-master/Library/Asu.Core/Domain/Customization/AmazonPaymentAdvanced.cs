using Asu.Core;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Customization
{
    public partial class AmazonPaymentAdvanced : BaseEntity
    {
        public string OrderReferenceId { get; set; }
        public string AmazonAuthorizationId { get; set; }
        public decimal OrderAmount { get; set; }
        public string OrderReferenceStatus { get; set; }
        public string AuthorizeStatus { get; set; }
        public string CaptureStatus { get; set; }
        public string AmazonCaptureId { get; set; }
        public decimal RefundedAmount { get; set; }
        public string AmazonRefundId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string LastError { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Order Order { get; set; }
        public virtual Order SecondOrder { get; set; }
    }
}
