using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Catalog
{
    /// <summary>
    /// Represents a specification attribute type
    /// </summary>
    public enum SpecificationAttributeType
    {
        /// <summary>
        /// Option
        /// </summary>
        Option = 0,
        /// <summary>
        /// Custom text
        /// </summary>
        CustomText = 10,
        /// <summary>
        /// Custom HTML text
        /// </summary>
        CustomHtmlText = 20,
        /// <summary>
        /// Hyperlink
        /// </summary>
        Hyperlink = 30
    }
}
