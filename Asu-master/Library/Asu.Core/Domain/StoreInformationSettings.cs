using Asu.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain
{
    public class StoreInformationSettings : ISettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether store is closed
        /// </summary>
        public bool StoreClosed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether administrators can visit a closed store
        /// </summary>
        public bool StoreClosedAllowForAdmins { get; set; }

        /// <summary>
        /// Gets or sets a default store theme
        /// </summary>
        public string DefaultStoreTheme { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether customers are allowed to select a theme
        /// </summary>
        public bool AllowCustomerToSelectTheme { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether responsive design supported (a graphical theme should also support it)
        /// </summary>
        public bool ResponsiveDesignSupported { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether mini profiler should be displayed in public store (used for debugging)
        /// </summary>
        public bool DisplayMiniProfilerInPublicStore { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we should display warnings about the new EU cookie law
        /// </summary>
        public bool DisplayEuCookieLawWarning { get; set; }

        /// <summary>
        /// Gets or sets a value of Facebook page URL of the site
        /// </summary>
        public string FacebookLink { get; set; }

        /// <summary>
        /// Gets or sets a value of Twitter page URL of the site
        /// </summary>
        public string TwitterLink { get; set; }

        /// <summary>
        /// Gets or sets a value of YouTube channel URL of the site
        /// </summary>
        public string YoutubeLink { get; set; }

        /// <summary>
        /// Gets or sets a value of Google+ page URL of the site
        /// </summary>
        public string GooglePlusLink { get; set; }

        #region WC

        /// <summary>
        /// Gets or sets a value indicating whether vehicle feature is allowed to the current store
        /// </summary>
        public bool VehicleSupported { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether product groups are allowed to the current store
        /// </summary>
        public bool ProductGroupSupported { get; set; }

        #endregion
    }
}
