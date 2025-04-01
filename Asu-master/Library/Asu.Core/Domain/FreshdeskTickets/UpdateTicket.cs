namespace Asu.Core.Domain.FreshdeskTickets
{
    using Newtonsoft.Json;

    [JsonObject]
    public class UpdateTicket
    {
        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("group_id")]
        public long GroupId { get; set; }
    }
}