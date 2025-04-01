using System.Collections.Generic;
using Asu.Framework.Mvc;
using Asu.Web.Models.Common;

namespace Asu.Web.Models.Checkout
{
    public partial class CheckoutShippingAddressModel : BaseNopModel
    {
        public CheckoutShippingAddressModel()
        {
            ExistingAddresses = new List<AddressModel>();
            NewAddress = new AddressModel();
        }

        public IList<AddressModel> ExistingAddresses { get; set; }

        public AddressModel NewAddress { get; set; }

        public bool NewAddressPreselected { get; set; }

        public bool AllowPickUpInStore { get; set; }
        public string PickUpInStoreFee { get; set; }
        public bool PickUpInStore { get; set; }
    }
}