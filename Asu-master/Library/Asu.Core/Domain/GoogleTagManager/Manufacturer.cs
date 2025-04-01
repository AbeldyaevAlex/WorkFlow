using Newtonsoft.Json;

namespace Asu.Core.Domain.GoogleTagManager
{
    [JsonObject]
    public class Manufacturer
    {
        [JsonProperty("brandId")]
        public int ManufacturerId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
