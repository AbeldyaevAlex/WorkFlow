namespace Asu.Core.Domain.FreshdeskTickets
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    [JsonObject]
    public class Ticket
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("cc_email")]
        public CcEmail CcEmail { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("delta")]
        public bool Delta { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("description_html")]
        public string DescriptionHtml { get; set; }

        [JsonProperty("display_id")]
        public int DisplayId { get; set; }

        [JsonProperty("due_by")]
        public DateTime DueBy { get; set; }

        [JsonProperty("email_config_id")]
        public object EmailConfigId { get; set; }

        [JsonProperty("frDueBy")]
        public DateTime FrDueBy { get; set; }

        [JsonProperty("fr_escalated")]
        public bool FrEscalated { get; set; }

        [JsonProperty("group_id")]
        public object GroupId { get; set; }

        [JsonProperty("isescalated")]
        public bool IsEscalated { get; set; }

        [JsonProperty("notes")]
        public List<object> Notes { get; set; }

        [JsonProperty("owner_id")]
        public object OwnerId { get; set; }

        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("requester_id")]
        public long RequesterId { get; set; }

        [JsonProperty("responder_id")]
        public object ResponderId { get; set; }

        [JsonProperty("source")]
        public int Source { get; set; }

        [JsonProperty("spam")]
        public bool IsSpam { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("status_name")]
        public string StatusName { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("ticket_type")]
        public string TicketType { get; set; }

        [JsonProperty("to_email")]
        public object ToEmail { get; set; }

        [JsonProperty("trained")]
        public bool Trained { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("urgent")]
        public bool Urgent { get; set; }

        [JsonProperty("requester_status_name")]
        public string RequesterStatusName { get; set; }

        [JsonProperty("priority_name")]
        public string PriorityName { get; set; }

        [JsonProperty("source_name")]
        public string SourceName { get; set; }

        [JsonProperty("requester_name")]
        public string RequesterName { get; set; }

        [JsonProperty("responder_name")]
        public string ResponderName { get; set; }

        [JsonProperty("product_id")]
        public long? ProductId { get; set; }

        [JsonProperty("to_emails")]
        public object ToEmails { get; set; }

        [JsonProperty("attachments")]
        public List<object> Attachments { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }
    }
}