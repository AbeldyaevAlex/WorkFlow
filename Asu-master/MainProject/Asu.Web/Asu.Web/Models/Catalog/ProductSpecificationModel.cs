using Asu.Framework.Mvc;
using System.Collections.Generic;

namespace Asu.Web.Models.Catalog
{
    public partial class ProductSpecificationModel : BaseNopModel
    {
        public int SpecificationAttributeId { get; set; }

        public string SpecificationAttributeName { get; set; }

        //this value is already HTML encoded
        public string ValueRaw { get; set; }

        public IList<ProductSpecificationModel> SpecificationAttributeDescriptions { get; set; }
    }
}