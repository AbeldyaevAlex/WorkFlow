using System.Collections.Generic;
using Asu.Core.Domain.GoogleTagManager;
using Asu.Core.Domain.Orders;
using Product = Asu.Core.Domain.Catalog.Product;
using Category = Asu.Core.Domain.Catalog.Category;
using Manufacturer = Asu.Core.Domain.Catalog.Manufacturer;

namespace Asu.Services.Customization
{
    public interface IGoogleTagManagerService
    {
        PageType PageType { get; }

        DataLayer GetDataLayer();

        string GetDataLayerScript();

        string GetDataLayerPushScript();

        void SetPage(PageType page, GroupingPageType pageType);

        void SetShoppingCartData(PageType page, GroupingPageType pageType, int? removeProductId = null);

        void SetShoppingCartData(IList<ShoppingCartItem> cart, PageType page, GroupingPageType pageType, int? removeProductId = null);

        void SetOrderData(PageType page, GroupingPageType pageType, Asu.Core.Domain.Orders.Order order);

        void SetOrderData(PageType page, GroupingPageType pageType, int orderId);

        void SetProductData(Product product, bool usesImageLoader = false, PageType page = PageType.Product, GroupingPageType pageType = GroupingPageType.ProductPages);

        void SetProductData(int productId, bool usesImageLoader = false, PageType page = PageType.Product, GroupingPageType pageType = GroupingPageType.ProductPages);

        void SetCategoryData(Category category, PageType page = PageType.Category, GroupingPageType pageType = GroupingPageType.SearchPages);

        void SetManufacturerData(Manufacturer manufacturer, PageType page = PageType.Manufacturer, GroupingPageType pageType = GroupingPageType.SearchPages);

        void SetEcommerceImpressions(List<Impression> impressions);

        void SetProductIds(int[] productIds);
    }
}
