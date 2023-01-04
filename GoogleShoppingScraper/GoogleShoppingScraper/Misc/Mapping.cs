using GoogleShoppingScraper.Properties;
using GoogleShoppingScraper.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleShoppingScraper.Misc
{
    public static class Mapping
    {
        private static List<BrandMapping> brands;

        public static List<BrandMapping> Brands
        {
            get
            {
                if (brands != null)
                {
                    return brands;
                }

                using (var context = new DatabaseContext(Settings.Default.ScraperConnectionString))
                {
                    brands = context.BrandMapping.ToList();
                }

                return brands;
            }
        }
    }
}
