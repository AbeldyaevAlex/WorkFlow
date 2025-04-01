using System.Collections.Generic;
using Newtonsoft.Json;

namespace Asu.Core.Domain.GoogleTagManager
{
    [JsonObject]
    public class ShoppingCart
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("subTotal")]
        public decimal SubTotal { get; set; }

        [JsonProperty("customerEmail")]
        public string CustomerEmail { get; set; }

        [JsonProperty("removeProductId")]
        public int? RemoveProductId { get; set; }
    }
}
