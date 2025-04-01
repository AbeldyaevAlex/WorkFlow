namespace Asu.Core.Domain.FreshdeskTickets
{
    using Newtonsoft.Json;

    [JsonObject]
    public class Tag
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}