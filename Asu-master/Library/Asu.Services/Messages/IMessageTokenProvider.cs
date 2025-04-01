using System.Collections.Generic;
using Asu.Core.Domain.Blogs;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Customization;
using Asu.Core.Domain.Forums;
using Asu.Core.Domain.Messages;
using Asu.Core.Domain.News;
using Asu.Core.Domain.Orders;
using Asu.Core.Domain.Shipping;
using Asu.Core.Domain.Stores;

namespace Asu.Services.Messages
{
    using Asu.Core.Domain.Returns;
    using Asu.Core.Domain.SalesQuotes;

    public partial interface IMessageTokenProvider
    {
        void AddStoreTokens(IList<Token> tokens, Store store, EmailAccount emailAccount);

        void AddManualOrderTokens(IList<Token> tokens, Store store, ManualOrderShipment shipment, int languageId);

        void AddOrderTokens(IList<Token> tokens, Order order, int languageId, int vendorId = 0);

        void AddShipmentTokens(IList<Token> tokens, Shipment shipment, int languageId);

        void AddOrderNoteTokens(IList<Token> tokens, OrderNote orderNote);

        void AddRecurringPaymentTokens(IList<Token> tokens, RecurringPayment recurringPayment);

        void AddGiftCardTokens(IList<Token> tokens, GiftCard giftCard);

        void AddCustomerTokens(IList<Token> tokens, Customer customer);

        void AddNewsLetterSubscriptionTokens(IList<Token> tokens, NewsLetterSubscription subscription);

        void AddProductReviewTokens(IList<Token> tokens, ProductReview productReview);

        void AddBlogCommentTokens(IList<Token> tokens, BlogComment blogComment);

        void AddNewsCommentTokens(IList<Token> tokens, NewsComment newsComment);

        void AddProductTokens(IList<Token> tokens, Product product, int languageId);

        void AddAttributeCombinationTokens(IList<Token> tokens, ProductVariantAttributeCombination combination, int languageId);

        void AddForumTokens(IList<Token> tokens, Forum forum);

        void AddForumTopicTokens(IList<Token> tokens, ForumTopic forumTopic,
            int? friendlyForumTopicPageIndex = null, int? appendedPostIdentifierAnchor = null);

        void AddForumPostTokens(IList<Token> tokens, ForumPost forumPost);

        void AddPrivateMessageTokens(IList<Token> tokens, PrivateMessage privateMessage);

        void AddBackInStockTokens(IList<Token> tokens, BackInStockSubscription subscription);

        string[] GetListOfCampaignAllowedTokens();

        string[] GetListOfAllowedTokens();

        #region WC
        void AddOrderWithRebatesTokens(IList<Token> tokens, OrderWithRebates orderWithRebates);
        void AddOrderProductToReviewTokens(IList<Token> tokens, OrderProductToReview orderProductToReview);
        void AddOrderShipmentEtaTokens(IList<Token> tokens, OrderShipmentEta orderShipmentEta);
        void AddReturnTokens(IList<Token> tokens, string orderNumber, string fullname, string message, string marketplace, string email, string phone);

        void AddSalesQuoteTokens(IList<Token> tokens, SalesQuote quote);

        void AddShipmentDelayedTokens(IList<Token> tokens, OrderItem[] orderItems, CrmSalesOrder order, int shipInDays, Store store, string email);

        void AddTopicTokens(IList<Token> tokens, int storeId);

        #endregion

        void AddOrderData(Order order, DynamicTemplateData data, int languageId, int vendorId = 0);
        void AddOrderCancelData(Order order, DynamicTemplateData data, int languageId, int vendorId = 0);
        void AddOrderCancelData(CrmSalesOrder order, DynamicTemplateData data, int languageId, int vendorId = 0);
        void AddShipmentData(Shipment shipment, DynamicTemplateData data, int languageId);
        void AddManualOrderShipmentData(CrmShipment crmShipment, int crmSalesOrderId, int storeId, DynamicTemplateData data, int languageId);
        void AddStoreData(Store store, DynamicTemplateData data, string fromEmail);
        void AddTopicData(int storeId, DynamicTemplateData data);
        void AddCustomerData(Customer customer, DynamicTemplateData data);
        void AddProductRecommendationsData(Order order, DynamicTemplateData data, int[] incomingProductIds, int productsCount, int languageId);

        void AddSalesOrderData(CrmSalesOrder order, DynamicTemplateData data, int languageId);
        void AddProductBackInStockData(BackInStockSubscription subscription, DynamicTemplateData dynamicTemplateData, int languageId);
        void AddProductBackInStockRecommendationsData(BackInStockSubscription subscription, DynamicTemplateData dynamicTemplateData, int v, int languageId);
        void AddManualOrderData(CrmSalesOrder order, DynamicTemplateData data, int languageId);
        void AddProductManualOrderRecommendationsData(CrmSalesOrder crmSalesOrder, NopStore noStore, DynamicTemplateData data, int productsCount, int languageId);
        void AddBackorderEtaData(Order order, DynamicTemplateData data, int languageId = 0);
    }
}
