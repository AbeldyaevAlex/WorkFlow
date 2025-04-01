using System.Collections.Generic;

namespace Asu.Web.Models.Vehicles
{
    public class ProductFitsModel
    {
        public string YearMakeModel { get; set; }

        public string Model { get; set; }

        public List<string> SubModels { get; set; }
    }
}