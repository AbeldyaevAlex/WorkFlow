using System.Collections.Generic;
using Asu.Framework.Mvc;
using Asu.Web.Models.Common;

namespace Asu.Web.Models.Checkout
{
    public partial class CheckoutBillingAddressModel : BaseNopModel
    {
        public CheckoutBillingAddressModel()
        {
            this.ExistingAddresses = new List<AddressModel>();
            this.NewAddress = new AddressModel();
        }

        public IList<AddressModel> ExistingAddresses { get; set; }

        public AddressModel NewAddress { get; set; }

        /// <summary>
        /// Used on one-page checkout page
        /// </summary>
        public bool NewAddressPreselected { get; set; }
    }
}