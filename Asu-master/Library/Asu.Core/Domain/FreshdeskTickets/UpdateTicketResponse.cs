namespace Asu.Core.Domain.FreshdeskTickets
{
    using Newtonsoft.Json;

    [JsonObject]
    public class UpdateTicketResponse
    {
        [JsonProperty("ticket")]
        public Ticket Ticket { get; set; }

        [JsonProperty("errors")]
        public string[] Errors { get; set; }
    }
}