using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Discounts
{
    public class CustomDiscountCategory : BaseEntity
    {
        /// <summary>
        /// Gets or sets discount identifier
        /// </summary>
        public int DiscountId { get; set; }

        /// <summary>
        /// Gets or sets category identifier
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or Sets category type identifier
        /// </summary>
        public short CategoryTypeId { get; set; }

        /// <summary>
        /// Gets or Sets created time
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or Sets updated time
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or Sets category type
        /// </summary>
        public DiscountCategoryType CategoryType
        {
            get { return (DiscountCategoryType)CategoryTypeId; }
            set { this.CategoryTypeId = (short)value; }
        }
    }
}
