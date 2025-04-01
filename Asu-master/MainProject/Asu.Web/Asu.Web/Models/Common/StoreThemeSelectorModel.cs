using System.Collections.Generic;
using Asu.Framework.Mvc;

namespace Asu.Models.Common
{
    public partial class StoreThemeSelectorModel : BaseNopModel
    {
        public StoreThemeSelectorModel()
        {
            AvailableStoreThemes = new List<StoreThemeModel>();
        }

        public IList<StoreThemeModel> AvailableStoreThemes { get; set; }

        public StoreThemeModel CurrentStoreTheme { get; set; }
    }
}