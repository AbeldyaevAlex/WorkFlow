using Newtonsoft.Json;

namespace Asu.Core.Domain.GoogleTagManager
{
    [JsonObject]
    public class DataLayerPush
    {
        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("ecommerce")]
        public Ecommerce Ecommerce { get; set; }
    }
}
