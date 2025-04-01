using Asu.Core.Domain.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Asu.Core.Domain.Catalog
{
    /// <summary>
    /// Represents a Product Group Club Price
    /// </summary>
    public partial class ProductGroupClubPrice : BaseEntity
    {
        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the store identifier (0 - all stores)
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Gets or sets the ClubMemberMinPrice
        /// </summary>
        public decimal ClubMemberMinPrice { get; set; }

        /// <summary>
        /// Gets or sets the ClubMemberMaxPrice
        /// </summary>
        public decimal ClubMemberMaxPrice { get; set; }

        /// <summary>
        /// Gets or sets the product
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Gets or sets the product
        /// </summary>
        public virtual Store Store { get; set; }

    }
}
