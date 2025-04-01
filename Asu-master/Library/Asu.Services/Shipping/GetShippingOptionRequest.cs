using System.Collections.Generic;
using Asu.Core.Domain.Common;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Directory;
using Asu.Core.Domain.Orders;

namespace Asu.Services.Shipping
{
    /// <summary>
    /// Represents a request for getting shipping rate options
    /// </summary>
    public partial class GetShippingOptionRequest
    {
        public GetShippingOptionRequest()
        {
            this.Items = new List<ShoppingCartItem>();
        }

        /// <summary>
        /// Gets or sets a customer
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets a shopping cart items
        /// </summary>
        public virtual IList<ShoppingCartItem> Items { get; set; }

        /// <summary>
        /// Gets or sets a shipping address (where we ship to)
        /// </summary>
        public virtual Address ShippingAddress { get; set; }

        /// <summary>
        /// Shipped from country
        /// </summary>
        public virtual Country CountryFrom { get; set; }
        /// <summary>
        /// Shipped from state/province
        /// </summary>
        public virtual StateProvince StateProvinceFrom { get; set; }
        /// <summary>
        /// Shipped from zip/postal code
        /// </summary>
        public virtual string ZipPostalCodeFrom { get; set; }
        /// <summary>
        /// Shipped from city
        /// </summary>
        public virtual string CityFrom { get; set; }
        /// <summary>
        /// Shipped from address
        /// </summary>
        public virtual string AddressFrom { get; set; }
    }
}
