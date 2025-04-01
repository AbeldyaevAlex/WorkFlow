using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Framework
{
    using Asu.Core.Domain.Stores;
    using Asu.Core;
    using Asu.Services.Stores;
    using System.IO;
    using System.Web.Hosting;

    /// <summary>
    /// Store context for web application
    /// </summary>
    public partial class WebStoreContext : IStoreContext
    {
        private readonly IStoreService _storeService;
        private readonly IWebHelper _webHelper;

        private Store _cachedStore;

        public WebStoreContext(IStoreService storeService, IWebHelper webHelper)
        {
            this._storeService = storeService;
            this._webHelper = webHelper;
        }

        /// <summary>
        /// Gets or sets the current store
        /// </summary>
        public virtual Store CurrentStore
        {
            get
            {
                if (_cachedStore != null)
                    return _cachedStore;

                //ty to determine the current store by HTTP_HOST
                var host = _webHelper.ServerVariables("HTTP_HOST");
                var allStores = _storeService.GetAllStores();
                var store = allStores.FirstOrDefault(s => s.ContainsHostValue(host));

                if (store == null)
                {
                    var path = HostingEnvironment.MapPath("~/Stores/storeid.txt");
                    if (!string.IsNullOrEmpty(path) && File.Exists(path))
                    {
                        var lines = File.ReadAllLines(path);
                        if (lines.Length > 0 && int.TryParse(lines[0], out var storeId))
                        {
                            store = allStores.FirstOrDefault(s => s.Id == storeId);
                        }
                    }

                    if (store == null)
                    {
                        //load the first found store
                        store = allStores.FirstOrDefault();
                    }
                }

                if (store == null)
                    throw new Exception("No store could be loaded");

                _cachedStore = store;
                return _cachedStore;
            }
        }
    }
}
