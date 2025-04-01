using System;
using System.Collections.Generic;
using System.Linq;
using Asu.Framework.Mvc;

namespace Asu.Web.Models.Returns
{
    public class ReturnRequestSummaryModel : BaseNopEntityModel
    {
        public ReturnRequestSummaryModel()
        {
            this.ReturnRequestSummaryItems = new List<ReturnRequestSummaryItemModel>();
        }

        public DateTime CreatedOn { get; set; }

        public decimal OrderItemsAmount { get; set; }

        public decimal OrderShippingAmount { get; set; }

        public decimal OrderDiscountAmount { get; set; }

        public decimal OrderTotalAmount { get; set; }

        public string CrmOrderId { get; set; }

        public ReturnRequestModel ReturnRequest { get; set; }

        public decimal OriginalPaid
        {
            get
            {
                return this.ReturnRequestSummaryItems.Sum(i => i.OrderItem.Price * i.Quantity);
            }
        }

        public decimal RestockingFee
        {
            get
            {
                return this.ReturnRequestSummaryItems.Sum(i => i.OrderItem.Price * i.Quantity) - this.ItemsReturn;
            }
        }

        public decimal ItemsReturn
        {
            get { return this.ReturnRequestSummaryItems.Sum(i => i.ReturnAmount); }
        }

        public decimal ShippingReturn
        {
            get
            {
                decimal shippingReturn = 0;
                foreach (var item in this.ReturnRequestSummaryItems)
                {
                    if (item.ReturnReason.FaultType.Id == 1 && item.IsPurchaseOrderExist)
                    {
                        continue;
                    }

                    shippingReturn += this.OrderShippingAmount * (item.OrderItem.Price / (this.OrderTotalAmount - this.OrderShippingAmount));
                }

                return shippingReturn;
            }
        }

        public decimal TotalReturn
        {
            get { return this.ItemsReturn + this.ShippingReturn - this.OrderDiscountAmount * (this.OrderDiscountAmount / (this.OrderTotalAmount - this.OrderShippingAmount)); }
        }

        public List<ReturnRequestSummaryItemModel> ReturnRequestSummaryItems { get; set; }
    }
}