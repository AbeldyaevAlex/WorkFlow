using Newtonsoft.Json;

namespace Asu.Core.Domain.Klaviyo
{
    [JsonObject]
    public class SubscriptionPayload
    {
        [JsonProperty("profiles")]
        public SubscriptionProfile Profiles { get; set; }
    }
}
