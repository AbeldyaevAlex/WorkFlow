using System;

namespace Asu.Core.Domain.Customization
{
    public partial class ProductReviewCustomerNotification : BaseEntity
    {
        public int OrderId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}