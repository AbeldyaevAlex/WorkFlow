using System.Collections.Generic;

namespace Asu.Web.Models.Vehicles
{
    public class ProductFitmentGroupModel
    {
        public string MakeModel { get; set; }

        public List<ProductFitmentYearModel> Years { get; set; }
    }
}