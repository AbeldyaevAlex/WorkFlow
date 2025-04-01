using System;
using Newtonsoft.Json;

namespace Asu.Core.Domain.GoogleTagManager
{
    [JsonObject]
    public class DataLayer
    {
        public DataLayer()
        {
        }

        [JsonIgnore]
        public PageType PageType { get; set; }

        [JsonIgnore]
        public GroupingPageType ContentGroupingPageType { get; set; }

        [JsonProperty("page")]
        public string Page => this.PageType.ToString();

        [JsonProperty("pageType")]
        public string GroupingPageType => this.ContentGroupingPageType.ToString();

        [JsonProperty("customerGuid")]
        public Guid CustomerGuid { get; set; }

        [JsonProperty("admin")]
        public bool IsAdmin { get; set; }

        [JsonProperty("guest")]
        public bool IsGuest { get; set; }

        [JsonProperty("shoppingCart")]
        public ShoppingCart ShoppingCart { get; set; }

        [JsonProperty("order")]
        public Order Order { get; set; }

        [JsonProperty("product")]
        public Product Product { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("brand")]
        public Manufacturer Manufacturer { get; set; }

        [JsonProperty("customerEmail")]
        public string CustomerEmail { get; set; }

        [JsonProperty("customerFirstName")]
        public string customerFirstName { get; set; }

        /*#region GA eCommerce

        [JsonProperty("transactionId")]
        public int? OrderId { get; set; }

        [JsonProperty("transactionTotal")]
        public decimal? OrderTotal { get; set; }

        [JsonProperty("transactionTax")]
        public decimal? OrderTax { get; set; }

        [JsonProperty("transactionShipping")]
        public decimal? OrderShipping { get; set; }

        [JsonProperty("transactionProducts")]
        public List<TransactionProduct> TransactionProducts { get; set; }

        #endregion*/
    }
}
