namespace Asu.Core.Domain.FreshdeskTickets
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    [JsonObject]
    public class CcEmail
    {
        [JsonProperty("cc_emails")]
        public List<string> CcEmails { get; set; }

        [JsonProperty("fwd_emails")]
        public List<object> FwdEmails { get; set; }
    }
}