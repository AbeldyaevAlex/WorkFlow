using Newtonsoft.Json;

namespace Asu.Core.Domain.GoogleTagManager
{
    [JsonObject]
    public class Item : Product
    {
        [JsonProperty("qty")]
        public int Quantity { get; set; }

        //[JsonProperty("pictureUrl")]
        //public string PictureUrl { get; set; }

        [JsonProperty("subTotal")]
        public decimal SubTotal { get; set; }
    }
}
