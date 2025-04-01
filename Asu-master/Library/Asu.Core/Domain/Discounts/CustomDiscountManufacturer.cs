using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Discounts
{
    public partial class CustomDiscountManufacturer : BaseEntity
    {
        /// <summary>
        /// Gets or sets discount identifier
        /// </summary>
        public int DiscountId { get; set; }

        /// <summary>
        /// Gets or sets manufacturer identifier
        /// </summary>
        public int ManufacturerId { get; set; }

        /// <summary>
        /// Gets or Sets manufacturer type identifier
        /// </summary>
        public short ManufacturerTypeId { get; set; }

        /// <summary>
        /// Gets or Sets created time
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or Sets updated time
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or Sets manufaturer type
        /// </summary>
        public DiscountManufacturerType ManufacturerType
        {
            get { return (DiscountManufacturerType)ManufacturerTypeId; }
            set { this.ManufacturerTypeId = (short)value; }
        }
    }
}
