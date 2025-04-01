using Newtonsoft.Json;

namespace Asu.Core.Domain.Klaviyo
{
    [JsonObject]
    public class TrackProperties
    {
        [JsonProperty("item_name")]
        public string ItemName { get; set; }

        [JsonProperty("$value")]
        public decimal Value { get; set; }
    }
}
