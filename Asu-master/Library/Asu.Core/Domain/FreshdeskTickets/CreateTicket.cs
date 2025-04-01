namespace Asu.Core.Domain.FreshdeskTickets
{
    using Newtonsoft.Json;

    [JsonObject]
    public class CreateTicket
    {
        [JsonProperty("description_html")]
        public string Description { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("responder_id")]
        public long? ResponderId { get; set; }

        [JsonProperty("group_id")]
        public long? GroupId { get; set; }

        [JsonIgnore]
        public string CcEmail { get; set; }

        [JsonProperty("email_config_id")]
        public string EmailSupportId { get; set; }
    }
}
