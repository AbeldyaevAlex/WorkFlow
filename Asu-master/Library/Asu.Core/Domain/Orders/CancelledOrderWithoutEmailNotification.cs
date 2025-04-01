using Asu.Core.Domain.Customization;

namespace Asu.Core.Domain.Orders
{
    public partial class CancelledOrderWithoutEmailNotification : BaseEntity
    {
        public int OrderId { get; set; }
        public int StoreId { get; set; }
    }

}
