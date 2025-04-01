namespace Asu.Services.Vehicles
{
    using Asu.Core.Domain.Vehicles;

    public interface IVehicleHelper
    {
        bool GetVehicleFromCookies(out int yearId, out int makeId, out int modelId, out int subModelId, out bool showUniversal);
        void SetVehicleIdToCookies(int yearId, int makeId, int modelId, int subModelId, bool showUniversal);
        void SetVehicleSeoIdToCookies(int? yearId, int? makeId, int? modelId);
        void SetVehicleNameToCookies(int year, string makeName, string modelName, string subModelName);
        void SetVehicleSeoNameToCookies(int? year, string makeName, string modelName);
        void SetVehicleToCookies(Vehicle vehicle, bool showUniversal);
        void ClearVehicleCookies();
    }
}
