using Newtonsoft.Json;

namespace Asu.Core.Domain.GoogleTagManager
{
    [JsonObject]
    public class Category
    {
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("breadcrumb")]
        public string BreadCrumb { get; set; }
    }
}
