using Newtonsoft.Json;

namespace Asu.Core.Domain.GoogleTagManager
{
    [JsonObject]
    public class Impression
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("variant")]
        public string Variant { get; set; }

        [JsonProperty("list")]
        public string List { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }
    }
}
