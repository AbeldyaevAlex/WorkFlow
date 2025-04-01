using Newtonsoft.Json;
using System.Collections.Generic;

namespace Asu.Core.Domain.Klaviyo
{
    [JsonObject]
    public class StartedCheckoutProperties
    {
        public StartedCheckoutProperties()
        {
            Items = new List<ProductProperties>();
            Categories = new List<string>();
            ItemNames = new List<string>();
        }

        [JsonProperty("Categories", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Categories { get; set; }

        [JsonProperty("ItemNames", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ItemNames { get; set; }

        [JsonProperty("Items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ProductProperties> Items { get; set; }
    }
}