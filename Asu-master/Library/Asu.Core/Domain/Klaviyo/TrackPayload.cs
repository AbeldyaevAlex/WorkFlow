using Newtonsoft.Json;

namespace Asu.Core.Domain.Klaviyo
{
    [JsonObject]
    public class TrackPayload<T>
    {
        /// <summary>
        /// Public Key
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        //[JsonProperty("$event_id")]
        //public string EventId { get; set; }

        [JsonProperty("customer_properties", NullValueHandling = NullValueHandling.Ignore)]
        public CustomerProperties CustomerProperties { get; set; }

        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public T Properties { get; set; }
    }
}
