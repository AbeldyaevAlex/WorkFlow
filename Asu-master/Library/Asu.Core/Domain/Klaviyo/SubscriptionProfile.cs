using Newtonsoft.Json;

namespace Asu.Core.Domain.Klaviyo
{
    [JsonObject]
    public class SubscriptionProfile
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}