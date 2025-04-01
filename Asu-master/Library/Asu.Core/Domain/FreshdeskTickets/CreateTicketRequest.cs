namespace Asu.Core.Domain.FreshdeskTickets
{
    using Newtonsoft.Json;

    [JsonObject]
    public class CreateTicketRequest
    {
        [JsonProperty("helpdesk_ticket")]
        public CreateTicket Ticket { get; set; }

        [JsonProperty("cc_emails")]
        public string CcEmails { get; set; }
    }
}
