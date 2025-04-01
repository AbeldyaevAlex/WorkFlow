using Asu.Framework.Mvc;
using Asu.Web.Models.Common;

namespace Asu.Web.Models.Customer
{
    public partial class CustomerAddressEditModel : BaseNopModel
    {
        public CustomerAddressEditModel()
        {
            this.Address = new AddressModel();
        }
        public AddressModel Address { get; set; }
    }
}