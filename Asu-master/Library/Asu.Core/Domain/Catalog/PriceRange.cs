using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Catalog
{
    /// <summary>
    /// Represents a price range
    /// </summary>
    public partial class PriceRange
    {
        /// <summary>
        /// From
        /// </summary>
        public decimal? From { get; set; }
        /// <summary>
        /// To
        /// </summary>
        public decimal? To { get; set; }
    }
}
