using Asu.Core.Infrastructure;
using Asu.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Asu.Services.Helpers
{
    public class IpAddressHelper : IIpAddressHelper
    {
        private string[] disallowedIps;

        public IpAddressHelper()
        {
            InitDisallowedIps();
        }

        private void InitDisallowedIps()
        {
            var path = HostingEnvironment.MapPath("~/App_Data/crawlers.txt");
            if (string.IsNullOrEmpty(path))
            {
                throw new FileNotFoundException("crawlers.txt is not found.");
            }

            this.disallowedIps = File.ReadAllLines(path);
        }

        public bool IsSearchEngine()
        {
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            var ip = webHelper.GetCurrentIpAddress();
            var result = ip.StartsWith("66.249.65.");
            if (result)
            {
                return true;
            }

            return disallowedIps.Any(m => m == ip);
        }
    }
}
