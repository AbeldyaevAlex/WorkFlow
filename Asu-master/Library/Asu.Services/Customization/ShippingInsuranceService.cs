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
    public class ShippingInsuranceService : IShippingInsuranceService
    {
        private int[] productIds = { 15365223, 15425574, 15425575, 15425576, 15425577, 15425578, 15425579 };
        private Product[] insuranceProducts;
        private readonly IProductService productService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IWorkContext workContext;
        private readonly IStoreContext storeContext;
        private readonly ICustomHelper customHelper;

        public ShippingInsuranceService(IProductService productService, 
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
            this.insuranceProducts = this.productService.GetProductsByIds(this.productIds).ToArray();
        }

        public bool IsInsuranceApplied(IList<ShoppingCartItem> cart)
        {
            return cart.Any(sci => this.productIds.Contains(sci.ProductId));
        }

        public bool IsInsuranceApplied(IList<OrderItem> items)
        {
            return items.Any(sci => this.productIds.Contains(sci.ProductId));
        }

        public bool IsInsuranceApplied(ICollection<CrmSalesOrderLine> lines)
        {
            return lines.Any(sci => this.productIds.Contains(sci.ProductId.Value));
        }

        public bool IsInsuranceApplied()
        {
            var cart = this.workContext.CurrentCustomer.ShoppingCartItems
                .Where(sci => sci.ShoppingCartType == ShoppingCartType.ShoppingCart)
                .LimitPerStore(this.storeContext.CurrentStore.Id)
                .ToList();

            return cart.Any(sci => this.productIds.Contains(sci.ProductId));
        }

        public decimal ApplyShippingInsurance(bool? enable = true)
        {
            var storeId = this.storeContext.CurrentStore.Id;
            var product = this.GetInsuranceProduct(storeId);
            if (product == null || product.Id == 0)
            {
                return 0m;
            }

            var cart = this.workContext.CurrentCustomer.ShoppingCartItems
             .Where(sci => sci.ShoppingCartType == ShoppingCartType.ShoppingCart)
             .LimitPerStore(this.storeContext.CurrentStore.Id)
             .ToList();

            var insuranceItem = cart.FirstOrDefault(sci => productIds.Contains(sci.ProductId));
            if ((cart.Any() && insuranceItem != null && cart.All(sci => sci.Product.ProductExtra.IsWarranty) && cart.Any(sci => sci.ProductId == insuranceItem.ProductId)) || (cart.Any() && insuranceItem != null && !enable.Value))
            {
                this.shoppingCartService.DeleteShoppingCartItem(insuranceItem);
            }
            else if (insuranceItem != null && insuranceItem.ProductId != product.Id)
            {
                this.shoppingCartService.DeleteShoppingCartItem(insuranceItem);
                this.shoppingCartService.AddToCart(this.workContext.CurrentCustomer, product, ShoppingCartType.ShoppingCart, storeId, null, 0m, 1, false);
            }
            else if (cart.Any() && enable.Value && insuranceItem == null)
            {
                this.shoppingCartService.AddToCart(this.workContext.CurrentCustomer, product, ShoppingCartType.ShoppingCart, storeId, null, 0m, 1, false);
            }

            return product.Price;
        }

        public decimal GetInsuranceAmount()
        {
            var storeId = this.storeContext.CurrentStore.Id;
            var product = this.GetInsuranceProduct(storeId);

            return product != null ? product.Price : 0;
        }

        public bool IsProductInsurance(Product product)
        {
            return productIds.Any(i => i == product.Id);
        }

        public bool IsShowInsurance()
        {
            //var cookieValueA = this.customHelper.GetCookieValue("sdiexp_a");
            //var cookieValueB = this.customHelper.GetCookieValue("sdiexp_b");
            //var cookieValueC = this.customHelper.GetCookieValue("sdiexp_c");

            return true;//this.storeContext.CurrentStore.Id != (int)NopStore.Autoplicity || !string.IsNullOrEmpty(cookieValueA) || !string.IsNullOrEmpty(cookieValueB) || !string.IsNullOrEmpty(cookieValueC);
        }

        public Product GetInsuranceProduct(int stroreId)
        {
            var storeId = this.storeContext.CurrentStore.Id;
            var cart = this.workContext.CurrentCustomer.ShoppingCartItems
                 .Where(sci => sci.ShoppingCartType == ShoppingCartType.ShoppingCart)
                 .LimitPerStore(storeId)
                 .ToList();

            //var cookieValueA = this.customHelper.GetCookieValue("sdiexp_a");
            //var cookieValueB = this.customHelper.GetCookieValue("sdiexp_b");
            //var cookieValueC = this.customHelper.GetCookieValue("sdiexp_c");

            var productId = 0;
            var cartTotal = cart.Sum(sci => sci.Product.Price * sci.Quantity);

            if (cartTotal <= 100m)
            {
                productId = 15365223;
            }
            if (cartTotal > 100m && cartTotal <= 200m)
            {
                productId = 15425574;
            }
            else if (cartTotal > 200m && cartTotal <= 300m)
            {
                productId = 15425575;
            }
            else if (cartTotal > 300m && cartTotal <= 500m)
            {
                productId = 15425576;
            }
            else if (cartTotal > 500m)
            {
                productId = 15425577;
            }

            //else
            //{
            //    if (!string.IsNullOrEmpty(cookieValueA))
            //    {
            //        productId = 15365223;
            //    }
            //    else if (!string.IsNullOrEmpty(cookieValueB))
            //    {
            //        if (cartTotal <= 100m)
            //        {
            //            productId = 15365223;
            //        }
            //        else if (cartTotal > 100m && cartTotal <= 200m)
            //        {
            //            productId = 15425574;
            //        }
            //        else if (cartTotal > 200m && cartTotal <= 300m)
            //        {
            //            productId = 15425575;
            //        }
            //        else if (cartTotal > 300m && cartTotal <= 500m)
            //        {
            //            productId = 15425576;
            //        }
            //        else if (cartTotal > 500m)
            //        {
            //            productId = 15425577;
            //        }
            //    }
            //    else if (!string.IsNullOrEmpty(cookieValueC))
            //    {
            //        if (cartTotal <= 100m)
            //        {
            //            productId = 15425575;
            //        }
            //        else if (cartTotal > 100m && cartTotal <= 200m)
            //        {
            //            productId = 15425576;
            //        }
            //        else if (cartTotal > 200m && cartTotal <= 300m)
            //        {
            //            productId = 15425577;
            //        }
            //        else if (cartTotal > 300m && cartTotal <= 500m)
            //        {
            //            productId = 15425578;
            //        }
            //        else if (cartTotal > 500m)
            //        {
            //            productId = 15425579;
            //        }
            //    }
            //}

            var product = this.productService.GetProductById(productId);

            return product;
        }
    }
}
