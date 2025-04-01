namespace Asu.Web.Models.SimpleCheckout
{
    using System.Collections.Generic;

    using Asu.Framework.Mvc;

    public class CheckoutAddressModel : BaseNopModel
    {
        public CheckoutAddressModel()
        {
            this.ExistingAddresses = new List<AddressModel>();
            this.SelectedAddress = new AddressModel();
            this.NewAddress = new AddressModel();
        }

        public IList<AddressModel> ExistingAddresses { get; set; }

        public AddressModel SelectedAddress { get; set; }

        public AddressModel NewAddress { get; set; }

        public AddressType Type { get; set; }
    }
}