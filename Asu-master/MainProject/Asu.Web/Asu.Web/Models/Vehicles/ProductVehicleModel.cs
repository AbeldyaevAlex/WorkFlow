using Asu.Framework.Mvc;

namespace Asu.Web.Models.Vehicles
{
    public class ProductVehicleModel : BaseNopModel
    {
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string SubModel { get; set; }
    }
}