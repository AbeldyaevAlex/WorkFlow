namespace Asu.Web.Models.Returns
{
    public class ReturnRequestSummaryItemModel : ReturnRequestItemModel
    {
        public ReturnReasonModel ReturnReason { get; set; }

        public bool IsPurchaseOrderExist { get; set; }

        public decimal ReturnAmount
        {
            get
            {
                // customer fault and PO created
                if ((this.ReturnReason.FaultType.Id == 1) && this.IsPurchaseOrderExist)
                {
                    return (this.OrderItem.Price * this.Quantity) * 0.85m;
                }
                
                return this.OrderItem.Price * this.Quantity;
            }
        }
    }
}