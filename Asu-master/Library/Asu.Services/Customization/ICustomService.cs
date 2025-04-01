using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Customization;
using Asu.Core.Domain.Orders;
using System.Collections.Generic;

namespace Asu.Services.Customization
{
    using Asu.Core.Domain.Returns;
    using Asu.Core.Domain.Vehicles;

    public interface ICustomService
    {
        void AddWelcomeCookie();
        void AddSignCouponNotificationToDb(string email);
        List<string> GetAllManufacturerFirstSymbols();
        List<string> GetAllManufacturerFirstSymbolsByDiscount(int discountId);
        List<string> GetAllManufacturerFirstAlphabetSymbols();
        List<Manufacturer> GetAllManufacturersWithFirstSymbol(string firstSymbol, bool isSymbolNumeric = false);
        List<Manufacturer> GetAllManufacturersByDiscount(int discountId);
        List<Manufacturer> GetAllManufacturersByDiscountWithFirstSymbol(string firstSymbol, int discountId, bool isSymbolNumeric = false);
        bool CheckSymbolForNumeric(string symbol);
        List<SolrCategory> GetAllCategories();
        List<int> GetAllChildCategoriesIdList(int parentCategoryId);
        byte[] GetAdditionalImageForProduct(int productId);
        string GetProductAdditionalImageName(int productId);
        string GetProductGoogleImageName(int productId);
        bool GetProductCashRebateAmount(int productId, out decimal rebateAmount);
        void SaveProductCashRebates(Order order);
        IList<OrderWithRebates> GetOrdersWithRebates();
        void NotifyOrderWithRebatesCustomer(OrderWithRebates orderWithRebates);
        void InsertOrderWithRebatesNotification(OrderWithRebatesNotification notification);
        IList<OrderProductToReview> GetOrderProductsToReview(int count = 0);
        void NotifyProductReviewCustomer(OrderProductToReview orderProductToReview);
        void InsertProductReviewCustomerNotification(ProductReviewCustomerNotification notification);
        IList<OrderShipmentEta> GetOrderShipmentEta();
        void NotifyOrderShipmentEtaCustomer(OrderShipmentEta orderShipmentEta);
        void InsertOrderShipmentEtaNotification(OrderEtaNotification notification);

        void InsertOrderReviewNotification(Order notification);

        Locker GetLocker(string lockerName);
        bool IsLocked(string lockerName, int maxTimeoutSeconds);
        void SetLocked(string lockerName);
        bool SetLockedIfUnlocked(string lockerName, int maxTimeoutSeconds);
        void SetUnlocked(string lockerName);
        void InsertOrderExtra(OrderExtra orderExtra);
        void UpdateOrderExtra(OrderExtra orderExtra);
        OrderExtra GetOrderExtra(int orderId);
        //void CallKountService(int? orderId, string shippingMethodName, Customer customer, ProcessPaymentRequest processPaymentRequest, ProcessPaymentResult paymentResult);
        bool PrepareSearchThumbPicture(int pictureId);

        void InsertEbayOrderDeliveryNotification(CrmSalesOrder order);

        void InsertEbayMarketplaceAccountDeletionNotification(EbayMarketplaceAccountDeletionNotification notification);
    }
}
