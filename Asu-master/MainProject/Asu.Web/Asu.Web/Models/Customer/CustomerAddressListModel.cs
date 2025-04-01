using System.Collections.Generic;
using Asu.Framework.Mvc;
using Asu.Web.Models.Common;

namespace Asu.Web.Models.Customer
{
    public partial class CustomerAddressListModel : BaseNopModel
    {
        public CustomerAddressListModel()
        {
            Addresses = new List<AddressModel>();
        }

        public IList<AddressModel> Addresses { get; set; }
    }
}