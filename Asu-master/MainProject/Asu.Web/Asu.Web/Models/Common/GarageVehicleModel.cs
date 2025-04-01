using Asu.Core.Domain.Vehicles;
using Asu.Framework.Mvc;

namespace Asu.Web.Models.Common
{
    public class GarageVehicleModel : BaseNopModel
    {
        public Vehicle Vehicle { get; set; }
        public string VehicleName { get; set; }
        public bool IsMain { get; set; }
    }
}