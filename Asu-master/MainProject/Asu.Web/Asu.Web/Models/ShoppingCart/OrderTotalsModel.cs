using System.Collections.Generic;
using Asu.Framework.Mvc;

namespace Asu.Web.Models.ShoppingCart
{
    public partial class OrderTotalsModel : BaseNopModel
    {
        public OrderTotalsModel()
        {
            TaxRates = new List<TaxRate>();
            GiftCards = new List<GiftCard>();
        }
        public bool IsEditable { get; set; }

        public string SubTotal { get; set; }

        public string SubTotalDiscount { get; set; }
        public bool AllowRemovingSubTotalDiscount { get; set; }

        public string Shipping { get; set; }
        public bool RequiresShipping { get; set; }
        public string SelectedShippingMethod { get; set; }

        public string PaymentMethodAdditionalFee { get; set; }

        public string Tax { get; set; }
        public IList<TaxRate> TaxRates { get; set; }
        public bool DisplayTax { get; set; }
        public bool DisplayTaxRates { get; set; }

        public bool EstimateShippingEnabled { get; set; }

        public IList<GiftCard> GiftCards { get; set; }

        public string OrderTotalDiscount { get; set; }
        public bool AllowRemovingOrderTotalDiscount { get; set; }
        public int RedeemedRewardPoints { get; set; }
        public string RedeemedRewardPointsAmount { get; set; }

        public int WillEarnRewardPoints { get; set; }

        public string OrderTotal { get; set; }

        public bool AllItemsFreeShipping { get; set; }

        public decimal OrderTotalValue { get; set; }

        #region Nested classes

        public partial class TaxRate: BaseNopModel
        {
            public string Rate { get; set; }
            public string Value { get; set; }
        }

        public partial class GiftCard : BaseNopEntityModel
        {
            public string CouponCode { get; set; }
            public string Amount { get; set; }
            public string Remaining { get; set; }
        }
        #endregion

        #region WC

        public string ShippingDiscount { get; set; }

        public bool ApplyClubPricing { get; set; }

        public bool ApplyPackageDeliveryInsurance { get; set; }

        public bool ApplyReturnExtension { get; set; }

        public bool ShowInsurance { get; set; }

        public bool IsInsuranceEditable { get; set; }

        public bool ShowReturnExtension { get; set; }

        public decimal ShippingDeliveryInsuranceAmount { get; set; }

        public string ShippingDeliveryInsurance { get; set; }

        public decimal ReturnExtensionAmount { get; set; }

        public string ReturnExtension { get; set; }

        #endregion
    }
}