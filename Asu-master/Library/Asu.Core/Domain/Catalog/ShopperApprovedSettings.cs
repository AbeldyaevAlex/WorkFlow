using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Catalog
{
    using Asu.Core.Configuration;

    public class ShopperApprovedSettings : ISettings
    {
        public string SiteId { get; set; }

        public string Token { get; set; }

        public string Sort { get; set; }

        public string EndPoint { get; set; }
    }
}
