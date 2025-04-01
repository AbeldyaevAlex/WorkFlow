using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Services.Customization
{
    public interface IShippingInsuranceService
    {
        bool IsInsuranceApplied(IList<ShoppingCartItem> cart);

        bool IsInsuranceApplied(IList<OrderItem> items);

        bool IsInsuranceApplied(ICollection<CrmSalesOrderLine> lines);

        bool IsInsuranceApplied();

        decimal ApplyShippingInsurance(bool? enable = true);

        decimal GetInsuranceAmount();

        bool IsProductInsurance(Product product);

        Product GetInsuranceProduct(int stroreId);

        bool IsShowInsurance();
    }
}
