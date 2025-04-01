namespace Asu.Core.Domain.FreshdeskTickets
{
    using Newtonsoft.Json;

    [JsonObject]
    public class UpdateTicketRequest
    {
        [JsonProperty("helpdesk_ticket")]
        public UpdateTicket Ticket { get; set; }
    }
}