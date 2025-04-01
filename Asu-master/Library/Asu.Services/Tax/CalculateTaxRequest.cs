using Asu.Core.Domain.Common;
using Asu.Core.Domain.Customers;

namespace Asu.Services.Tax
{
    using Asu.Core.Domain.Catalog;

    /// <summary>
    /// Represents a request for tax calculation
    /// </summary>
    public partial class CalculateTaxRequest
    {
        /// <summary>
        /// Gets or sets a customer
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets an address
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Gets or sets a tax category identifier
        /// </summary>
        public int TaxCategoryId { get; set; }

        // WC.
        public Product Product { get; set; }
    }
}
