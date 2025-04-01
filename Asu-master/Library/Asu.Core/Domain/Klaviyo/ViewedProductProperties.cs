using Newtonsoft.Json;

namespace Asu.Core.Domain.Klaviyo
{
    [JsonObject]
    public class ViewedProductProperties
    {
        [JsonProperty("ProductName", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductName { get; set; }

        [JsonProperty("ProductID", NullValueHandling = NullValueHandling.Ignore)]
        public int ProductId { get; set; }

        [JsonProperty("SKU", NullValueHandling = NullValueHandling.Ignore)]
        public string Sku { get; set; }

        [JsonProperty("Categories", NullValueHandling = NullValueHandling.Ignore)]
        public string Categories { get; set; }

        [JsonProperty("ImageURL", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageURL { get; set; }

        [JsonProperty("URL", NullValueHandling = NullValueHandling.Ignore)]
        public string URL { get; set; }

        [JsonProperty("Brand", NullValueHandling = NullValueHandling.Ignore)]
        public string Brand { get; set; }

        [JsonProperty("Price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Price { get; set; }

        //[JsonProperty("CompareAtPrice", NullValueHandling = NullValueHandling.Ignore)]
        //public decimal CompareAtPrice { get; set; }
    }
}