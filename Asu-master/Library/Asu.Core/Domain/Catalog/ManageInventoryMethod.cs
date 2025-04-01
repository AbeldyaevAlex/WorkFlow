using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Catalog
{
    public enum ManageInventoryMethod
    {
        /// <summary>
        /// Don't track inventory for product
        /// </summary>
        DontManageStock = 0,
        /// <summary>
        /// Track inventory for product
        /// </summary>
        ManageStock = 1,
        /// <summary>
        /// Track inventory for product by product attributes
        /// </summary>
        ManageStockByAttributes = 2,
    }
}
