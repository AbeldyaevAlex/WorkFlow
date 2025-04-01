using Newtonsoft.Json;

namespace Asu.Core.Domain.Klaviyo
{
    [JsonObject]
    public class CustomerProperties
    {
        [JsonProperty("$id")]
        public string Id { get; set; }

        [JsonProperty("$email")]
        public string Email { get; set; }

        [JsonProperty("$first_name")]
        public string FirstName { get; set; }

        [JsonProperty("$last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// eg: "+13239169023"
        /// </summary>
        [JsonProperty("$phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("$city")]
        public string City { get; set; }

        /// <summary>
        /// state, or other region
        /// </summary>
        [JsonProperty("$region")]
        public string Region { get; set; }

        [JsonProperty("$country")]
        public string Country { get; set; }

        [JsonProperty("$zip")]
        public string ZipCode { get; set; }

        ///// <summary>
        ///// url to a photo of a person
        ///// </summary>
        //[JsonProperty("$image")]
        //public string ImageUrl { get; set; }

        ///list of strings; eg: ['sms', 'email', 'web', 'directmail', 'mobile']
        //[JsonProperty("$consent")]
        //public string Consent { get; set; }
    }
}