using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Services.Customization
{
    public interface IReturnExtensionService
    {
        bool IsReturnExtensionApplied(IList<ShoppingCartItem> cart);

        bool IsReturnExtensionApplied(IList<OrderItem> items);

        bool IsReturnExtensionApplied(ICollection<CrmSalesOrderLine> lines);

        bool IsReturnExtensionApplied();

        decimal ApplyReturnExtension(bool? enable = null);

        decimal GetReturnExtensionAmount();

        bool IsProductReturnExtension(Product product);

        Product GetReturnExtensionProduct(int stroreId);

        bool IsShowReturnExtension();
    }
}
