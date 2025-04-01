namespace Asu.Web.Models.ProductGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class ProductGroupVariantDetailsModel
    {
        public int ProductId { get; set; }

        public string DisplayValue { get; set; }

        public int AttributeOptionId { get; set; }
    }
}