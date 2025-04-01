using Asu.Core.Domain.Common;
using Asu.Core.Domain.Customization;
using Asu.Core.Domain.Orders;

using System.Collections.Generic;

namespace Asu.Services.Customization
{
    public interface IAmazonPaymentsService
    {
        bool CheckAuthorizeStatus(OffAmazonPaymentsService.Model.Status authorizeStatus, out string message);

        bool LoginByAmazon(string orderReferenceId, string addressConsentToken, out string errorMessage);
        
        AmazonShippingMethodSet PrepareShippingMethodList(IList<ShoppingCartItem> cart, Address shippingAddress);

        AmazonPlaceOrderResult PlaceAmazonOrder(List<ShoppingCartItem> cart, string orderReferenceId, string addressConsentToken, string selectedMethod);

        AmazonPlaceOrderResult PlaceAmazonOrder(List<ShoppingCartItem> cart, string orderReferenceId, string selectedMethod);

        Address GetSelectedAddress(string orderReferenceId, string addressConsentToken);
    }
}
