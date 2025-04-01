namespace Asu.Web.Models.Catalog
{
    using System;

    public class LloydMatsConfigurator
    {
        public string BrandCode { get; set; }
        public string LineCode { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public Guid CustomerGuid { get; set; }
    }
}