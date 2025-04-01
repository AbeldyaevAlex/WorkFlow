namespace Asu.Core.Domain.FreshdeskTickets
{
    using Newtonsoft.Json;

    [JsonObject]
    public class GetTicketResponse
    {
        [JsonProperty("helpdesk_ticket")]
        public Ticket Ticket { get; set; }

        [JsonProperty("errors")]
        public string[] Errors { get; set; }
    }
}