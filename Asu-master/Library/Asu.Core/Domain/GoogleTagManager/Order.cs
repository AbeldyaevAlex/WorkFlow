using System.Collections.Generic;
using Newtonsoft.Json;

namespace Asu.Core.Domain.GoogleTagManager
{
    [JsonObject]
    public class Order
    {
        [JsonProperty("orderId")]
        public int OrderId { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("items2")]
        public List<GA4Item> Items2 { get; set; }

        [JsonProperty("subTotal")]
        public decimal SubTotal { get; set; }

        [JsonProperty("shipping")]
        public decimal Shipping { get; set; }

        [JsonProperty("tax")]
        public decimal Tax { get; set; }

        [JsonProperty("total")]
        public decimal Total { get; set; }

        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        [JsonProperty("customerEmail")]
        public string CustomerEmail { get; set; }

        [JsonProperty("customerFullName")]
        public string CustomerFullName { get; set; }

        [JsonProperty("customerFirstName")]
        public string CustomerFirstName { get; set; }

        [JsonProperty("customerLastName")]
        public string CustomerLastName { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("customerPhone")]
        public string CustomerPhone { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("coupon")]
        public string CouponCode { get; set; }

        [JsonProperty("ccbin")]
        public string CcBin { get; set; }
    }
}