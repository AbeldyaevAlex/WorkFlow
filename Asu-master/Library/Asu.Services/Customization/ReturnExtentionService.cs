using Asu.Core;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Customization;
using Asu.Core.Domain.Orders;
using Asu.Services.Catalog;
using Asu.Services.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Services.Customization
{
    public class ReturnExtensionService : IReturnExtensionService
    {
        private int[] productIds = { 15563513, 15563514, 15563515, 15563516, 15563517, 15563518, 15563519 };
        private Product[] ReturnExtensionProducts;
        private readonly IProductService productService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IWorkContext workContext;
        private readonly IStoreContext storeContext;
        private readonly ICustomHelper customHelper;

        public ReturnExtensionService(IProductService productService,
            IShoppingCartService shoppingCartService,
            IWorkContext workContext,
            IStoreContext storeContext,
            ICustomHelper customHelper)
        {
            this.productService = productService;
            this.shoppingCartService = shoppingCartService;
            this.workContext = workContext;
            this.storeContext = storeContext;
            this.customHelper = customHelper;
            this.ReturnExtensionProducts = this.productService.GetProductsByIds(this.productIds).ToArray();
        }

        public bool IsReturnExtensionApplied(IList<ShoppingCartItem> cart)
        {
            return cart.Any(sci => this.productIds.Contains(sci.ProductId));
        }

        public bool IsReturnExtensionApplied(IList<OrderItem> items)
        {
            return items.Any(sci => this.productIds.Contains(sci.ProductId));
        }

        public bool IsReturnExtensionApplied(ICollection<CrmSalesOrderLine> lines)
        {
            return lines.Any(sci => this.productIds.Contains(sci.ProductId.Value));
        }

        public bool IsReturnExtensionApplied()
        {
            var cart = this.workContext.CurrentCustomer.ShoppingCartItems
                .Where(sci => sci.ShoppingCartType == ShoppingCartType.ShoppingCart)
                .LimitPerStore(this.storeContext.CurrentStore.Id)
                .ToList();

            return cart.Any(sci => this.productIds.Contains(sci.ProductId));
        }

        public decimal ApplyReturnExtension(bool? enable = null)
        {
            var storeId = this.storeContext.CurrentStore.Id;
            var product = this.GetReturnExtensionProduct(storeId);
            if (product == null || product.Id == 0)
            {
                return 0m;
            }

            var cart = this.workContext.CurrentCustomer.ShoppingCartItems
             .Where(sci => sci.ShoppingCartType == ShoppingCartType.ShoppingCart)
             .LimitPerStore(this.storeContext.CurrentStore.Id)
             .ToList();

            var ReturnExtensionItem = cart.FirstOrDefault(sci => productIds.Contains(sci.ProductId));
            if ((cart.Any() && ReturnExtensionItem != null && cart.All(sci => sci.Product.ProductExtra.IsWarranty) && cart.Any(sci => sci.ProductId == ReturnExtensionItem.ProductId)) || (cart.Any() && ReturnExtensionItem != null && enable.HasValue && !enable.Value))
            {
                this.shoppingCartService.DeleteShoppingCartItem(ReturnExtensionItem);
            }
            else if (ReturnExtensionItem != null && ReturnExtensionItem.ProductId != product.Id)
            {
                this.shoppingCartService.DeleteShoppingCartItem(ReturnExtensionItem);
                this.shoppingCartService.AddToCart(this.workContext.CurrentCustomer, product, ShoppingCartType.ShoppingCart, storeId, null, 0m, 1, false);
            }
            else if (cart.Any() && enable.HasValue && enable.Value && ReturnExtensionItem == null)
            {
                this.shoppingCartService.AddToCart(this.workContext.CurrentCustomer, product, ShoppingCartType.ShoppingCart, storeId, null, 0m, 1, false);
            }

            return product.Price;
        }

        public decimal GetReturnExtensionAmount()
        {
            var storeId = this.storeContext.CurrentStore.Id;
            var product = this.GetReturnExtensionProduct(storeId);

            return product.Price;
        }

        public bool IsProductReturnExtension(Product product)
        {
            return productIds.Any(i => i == product.Id);
        }

        public bool IsShowReturnExtension()
        {
            //var cookieValueA = this.customHelper.GetCookieValue("sdiexp_a");
            //var cookieValueB = this.customHelper.GetCookieValue("sdiexp_b");
            //var cookieValueC = this.customHelper.GetCookieValue("sdiexp_c");

            return false;//this.storeContext.CurrentStore.Id != (int)NopStore.Autoplicity || !string.IsNullOrEmpty(cookieValueA) || !string.IsNullOrEmpty(cookieValueB) || !string.IsNullOrEmpty(cookieValueC);
        }

        public Product GetReturnExtensionProduct(int stroreId)
        {
            var storeId = this.storeContext.CurrentStore.Id;
            var cart = this.workContext.CurrentCustomer.ShoppingCartItems
                 .Where(sci => sci.ShoppingCartType == ShoppingCartType.ShoppingCart)
                 .LimitPerStore(storeId)
                 .ToList();

            var productId = 0;
            var cartTotal = cart.Sum(sci => sci.Product.Price * sci.Quantity);
            if (cartTotal <= 100m)
            {
                productId = 15563513;
            }
            if (cartTotal > 100m && cartTotal <= 200m)
            {
                productId = 15563514;
            }
            else if (cartTotal > 200m && cartTotal <= 300m)
            {
                productId = 15563515;
            }
            else if (cartTotal > 300m && cartTotal <= 500m)
            {
                productId = 15563516;
            }
            else if (cartTotal > 500m)
            {
                productId = 15563517;
            }

            var product = this.productService.GetProductById(productId);

            return product;
        }
    }
}
