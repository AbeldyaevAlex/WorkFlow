using Asu.Core.Configuration;

namespace Asu.Core.Domain.Customization
{
    public class GoogleMapsSettings : ISettings
    {
        public string ApiScriptLink { get; set; }

        public string PlaceId { get; set; }
    }
}
