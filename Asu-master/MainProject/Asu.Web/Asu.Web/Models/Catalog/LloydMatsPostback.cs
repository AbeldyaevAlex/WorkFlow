namespace Asu.Web.Models.Catalog
{
    using System;

    public class LloydMatsPostback
    {
        public LloydMatsItem[] Items { get; set; }
        public int TotalRows { get; set; }
        public string PostBackUrl { get; set; }
        public bool SubmitFormFlag { get; set; }
        public Guid Sid { get; set; }
        public bool Mobile { get; set; }

        public class LloydMatsItem
        {
            public string Description { get; set; }
            public decimal Wholesale { get; set; }
            public decimal Retail { get; set; }
            public string PartNumber { get; set; }
            public string LloydCode { get; set; }
            public decimal Weight { get; set; }
        }
    }
}
 