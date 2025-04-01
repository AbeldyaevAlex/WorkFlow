using Newtonsoft.Json;
using System.Collections.Generic;

namespace Asu.Core.Domain.Klaviyo
{
    [JsonObject]
    public class ProductProperties
    {
        public ProductProperties() 
        {
            //ProductCategories = new List<string>();
        }

        [JsonProperty("ProductID", NullValueHandling = NullValueHandling.Ignore)]
        public int ProductId { get; set; }

        [JsonProperty("SKU", NullValueHandling = NullValueHandling.Ignore)]
        public string Sku { get; set; }

        [JsonProperty("ProductName", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductName { get; set; }

        [JsonProperty("Quantity", NullValueHandling = NullValueHandling.Ignore)]
        public int Quantity { get; set; }

        [JsonProperty("ItemPrice", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ItemPrice { get; set; }

        [JsonProperty("RowTotal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal RowTotal { get; set; }

        [JsonProperty("ProductURL", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductURL { get; set; }

        [JsonProperty("ImageURL", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageURL { get; set; }

        //[JsonProperty("ProductCategories", NullValueHandling = NullValueHandling.Ignore)]
        //public List<string> ProductCategories { get; set; }

        [JsonProperty("ProductCategoryName", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductCategoryName { get; set; }

        [JsonProperty("ProductCategoryID", NullValueHandling = NullValueHandling.Ignore)]
        public int? ProductCategoryId { get; set; }
    }
}