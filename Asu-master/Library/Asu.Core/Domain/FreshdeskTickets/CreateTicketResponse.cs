namespace Asu.Core.Domain.FreshdeskTickets
{
    using Newtonsoft.Json;

    [JsonObject]
    public class CreateTicketResponse
    {
        [JsonProperty("helpdesk_ticket")]
        public Ticket Ticket { get; set; }

        [JsonIgnore]
        public string ErrorMessage { get; set; }
    }
}
