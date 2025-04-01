using Newtonsoft.Json;
using Asu.Core.Domain.Tax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Services.Messages
{
    public class From
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class Settings
    {
        [JsonProperty("enable")]
        public int Enable { get; set; }

        [JsonProperty("utm_source")]
        public string Source { get; set; }

        [JsonProperty("utm_medium")]
        public string Medium { get; set; }

        [JsonProperty("utm_content")]
        public string Content { get; set; }

        [JsonProperty("utm_campaign")]
        public string Campaign { get; set; }
    }

    public class Ganalytics
    {
        [JsonProperty("settings")]
        public Settings Settings { get; set; }
    }

    public class Filters
    {
        [JsonProperty("ganalytics")]
        public Ganalytics Ganalytics { get; set; }
    }

    public class To
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Bcc
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class SendGridProduct
    {
        public string ImageUrl { get; set; }

        public string Link { get; set; }

        public string Name { get; set; }

        public string Sku { get; set; }

        public string UnitPrice { get; set; }

        public int Quantity { get; set; }

        public string Price { get; set; }

        public string AttributeDescription { get; set; }
    }

    public class ShippmentProduct
    {
        public string ImageUrl { get; set; }

        public string Link { get; set; }

        public string Name { get; set; }

        [JsonProperty("SKU")]
        public string Sku { get; set; }

        public string Quantity { get; set; }
    }

    public class DynamicTemplateData
    {
        public DynamicTemplateData()
        {
            this.Products = new List<SendGridProduct>();
            this.RecommendedProducts = new List<RecommendedProduct>();
        }

        public string OrderNumber { get; set; }

        public string OrderCustomerEmail { get; set; }

        public string OrderBillingFirstName { get; set; }

        public string OrderBillingLastName { get; set; }

        public string OrderBillingPhoneNumber { get; set; }

        public string OrderBillingEmail { get; set; }

        public string OrderBillingFaxNumber { get; set; }

        public string OrderBillingCompany { get; set; }

        public string OrderBillingAddress1 { get; set; }

        public string OrderBillingAddress2 { get; set; }

        public string OrderBillingCity { get; set; }

        public string OrderBillingStateProvince { get; set; }

        public string OrderBillingZipPostalCode { get; set; }

        public string OrderBillingCountry { get; set; }

        public string OrderBillingCustomAttributes { get; set; }

        public string OrderShippingMethod { get; set; }

        public string OrderShippingFirstName { get; set; }

        public string OrderShippingLastName { get; set; }

        public string OrderShippingPhoneNumber { get; set; }

        public string OrderShippingEmail { get; set; }

        public string OrderShippingFaxNumber { get; set; }

        public string OrderShippingCompany { get; set; }

        public string OrderShippingAddress1 { get; set; }

        public string OrderShippingAddress2 { get; set; }

        public string OrderShippingCity { get; set; }

        public string OrderShippingStateProvince { get; set; }

        public string OrderShippingZipPostalCode { get; set; }

        public string OrderShippingCountry { get; set; }

        public string OrderShippingCustomAttributes { get; set; }

        public string OrderPaymentMethod { get; set; }

        public string OrderVatNumber { get; set; }

        public string OrderCancellationReason { get; set; }

        public string OrderETA { get; set; }

        [JsonProperty("products")]
        public List<SendGridProduct> Products { get; set; }

        public string SubTotal { get; set; }

        public string SubTotalDiscount { get; set; }

        public string CheckoutAttributeDescription { get; set; }

        public string ShipTotal { get; set; }

        public string PaymentMethodAdditionalFee { get; set; }

        public string TaxTotal { get; set; }

        public string OrderCreatedOn { get; set; }

        public string OrderURLForCustomer { get; set; }

        public string OrderTotal { get; set; }

        public string Discount { get; set; }

        public string TaxValue { get; set; }

        public string ShipmentShipmentNumber { get; set; }

        public string ShipmentTrackingNumber { get; set; }

        public string ShipmentURLForCustomer { get; set; }

        public string ProductReviewLink { get; set; }

        public string ProductReviewSourceName { get; set; }

        public string StoreName { get; set; }

        public string StoreURL { get; set; }

        public string StoreEmail { get; set; }

        public string StoreCompanyName { get; set; }

        public string StoreCompanyAddress { get; set; }

        public string StoreCompanyPhoneNumber { get; set; }

        public string StoreCompanyVat { get; set; }

        public string TopicAboutUs { get; set; }

        public string TopicPrivacyNotice { get; set; }

        public string TopicSalesTax { get; set; }

        public string TopicContactUs { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerUsername { get; set; }

        public string CustomerFullName { get; set; }

        public string CustomerFirstName { get; set; }

        public string CustomerLastName { get; set; }

        public string CustomerVatNumber { get; set; }

        public VatNumberStatus CustomerVatNumberStatus { get; set; }

        public string CustomerPasswordRecoveryURL { get; set; }

        public string CustomerAccountActivationURL { get; set; }

        public string WishlistURLForCustomer { get; set; }

        [JsonProperty("ProductRecommendations")]
        public List<RecommendedProduct> RecommendedProducts { get; set; }
    }

    public class RecommendedProduct
    {
        public string ImgLink { get; set; }

        public string ProductLink { get; set; }

        public string ProductName { get; set; }

        public string Price { get; set; }
    }

    public class Personalization
    {
        [JsonProperty("to")]
        public List<To> To { get; set; }

        [JsonProperty("bcc")]
        public List<Bcc> Bcc { get; set; }

        [JsonProperty("dynamic_template_data")]
        public DynamicTemplateData DynamicTemplateData { get; set; }
    }

    public class Root
    {
        [JsonProperty("from")]
        public From From { get; set; }

        [JsonProperty("filters")]
        public Filters Filters { get; set; }

        [JsonProperty("personalizations")]
        public List<Personalization> Personalizations { get; set; }

        [JsonProperty("template_id")]
        public string TemplateId { get; set; }
    }

}
