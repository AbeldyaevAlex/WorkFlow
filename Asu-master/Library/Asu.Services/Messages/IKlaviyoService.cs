using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Klaviyo;
using Asu.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Services.Messages
{
    public partial interface IKlaviyoService
    {
        /// <summary>
        /// Track Added To Cart Activity
        /// </summary>
        /// <param name="cusomerEmail">Cusomer Email</param>
        /// <param name="addToCartProperties">Product data</param>
        bool TrackAddToCartActivity(Customer customer, Product product, int quantity);
        bool TrackViewedProductActivity(Customer customer, Product product);
        bool TrackStartedCheckoutActivity(Customer customer, IList<ShoppingCartItem> shoppingCartItemList);
        bool TrackPlacedOrderActivity(Order order);
        bool TrackCanceledOrder(Order order);
        bool TrackOrderShipped(Order order);
        bool TrackOrderDelivered(Order order);
        bool IdentifyUpdate(Customer customer);
        void NewsLetterSubscription(string email, bool remove = false);
    }
}
