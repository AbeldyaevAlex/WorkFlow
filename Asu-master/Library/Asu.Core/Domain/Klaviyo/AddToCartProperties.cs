using Newtonsoft.Json;

namespace Asu.Core.Domain.Klaviyo
{
    [JsonObject]
    public class AddToCartProperties
    {
        [JsonProperty("AddedItemProductName", NullValueHandling = NullValueHandling.Ignore)]
        public string AddedItemProductName { get; set; }

        [JsonProperty("AddedItemProductID", NullValueHandling = NullValueHandling.Ignore)]
        public int AddedItemProductID { get; set; }

        [JsonProperty("AddedItemSKU", NullValueHandling = NullValueHandling.Ignore)]
        public string AddedItemSKU { get; set; }

        [JsonProperty("AddedItemImageURL", NullValueHandling = NullValueHandling.Ignore)]
        public string AddedItemImageURL { get; set; }

        [JsonProperty("AddedItemURL", NullValueHandling = NullValueHandling.Ignore)]
        public string AddedItemURL { get; set; }

        [JsonProperty("AddedItemPrice", NullValueHandling = NullValueHandling.Ignore)]
        public decimal AddedItemPrice { get; set; }

        [JsonProperty("AddedItemQuantity", NullValueHandling = NullValueHandling.Ignore)]
        public int AddedItemQuantity { get; set; }

        [JsonProperty("AddedItemCategoryName", NullValueHandling = NullValueHandling.Ignore)]
        public string AddedItemCategoryName { get; set; }
    }
}