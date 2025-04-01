using System.Collections.Generic;
using Asu.Core.Domain.Shipping;
using Asu.Framework.Mvc;

namespace Asu.Web.Models.Checkout
{
    public partial class CheckoutShippingMethodModel : BaseNopModel
    {
        public CheckoutShippingMethodModel()
        {
            this.ShippingMethods = new List<ShippingMethodModel>();
            this.Warnings = new List<string>();
        }

        public IList<ShippingMethodModel> ShippingMethods { get; set; }

        public IList<string> Warnings { get; set; }

        #region Nested classes

        public partial class ShippingMethodModel : BaseNopModel
        {
            public string ShippingRateComputationMethodSystemName { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Fee { get; set; }
            public bool Selected { get; set; }

            public ShippingOption ShippingOption { get; set; } 
        }
        #endregion
    }
}