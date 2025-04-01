using Asu.Framework.Mvc;
using System.Collections.Generic;

namespace Asu.Web.Models.Common
{
    public partial class HeaderGarageOptionModel : BaseNopModel
    {
        public HeaderGarageOptionModel()
        {
            VehicleGarage = new List<GarageVehicleModel>();
        }

        public List<GarageVehicleModel> VehicleGarage { get; set; }
    }
}