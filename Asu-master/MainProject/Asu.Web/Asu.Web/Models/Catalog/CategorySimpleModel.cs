using System.Collections.Generic;
using Asu.Framework.Mvc;

namespace Asu.Web.Models.Catalog
{
    public class CategorySimpleModel : BaseNopEntityModel
    {
        public CategorySimpleModel()
        {
            SubCategories = new List<CategorySimpleModel>();
        }

        public string Name { get; set; }

        public string SeName { get; set; }

        public int? NumberOfProducts { get; set; }

        public List<CategorySimpleModel> SubCategories { get; set; }
    }
}