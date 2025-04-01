namespace Asu.Services.Vehicles
{
    using System;
    using System.Web;

    using Asu.Core.Domain.Vehicles;

    public class VehicleHelper : IVehicleHelper
    {
        #region Constants

        private const string VEHICLE_ID_COOKIE_KEY = "WC.Vehicle.Id.Cookie";
        private const string VEHICLE_ID_COOKIE_PATTERN = "{0}|{1}|{2}|{3}|{4}";

        private const string VEHICLE_NAME_COOKIE_KEY = "WC.Vehicle.Name.Cookie";
        private const string VEHICLE_NAME_COOKIE_PATTERN = "{0}|{1}|{2}|{3}";

        private const string VEHICLE_SEO_NAME_COOKIE_KEY = "WC.Vehicle.Seo.Name.Cookie";
        private const string VEHICLE_SEO_ID_COOKIE_KEY = "WC.Vehicle.Seo.Id.Cookie";

        #endregion

        #region Fields
        
        private readonly HttpContextBase httpContext;

        #endregion

        public VehicleHelper(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
        }

        #region Methods

        public bool GetVehicleFromCookies(out int yearId, out int makeId, out int modelId, out int subModelId, out bool showUniversal)
        {
            yearId = makeId = modelId = subModelId = 0;
            showUniversal = false;

            var vehicleCookie = this.httpContext.Request.Cookies.Get(VEHICLE_ID_COOKIE_KEY);
            if (vehicleCookie == null || string.IsNullOrEmpty(vehicleCookie.Value))
            {
                return false;
            }

            var vehicleParts = vehicleCookie.Value.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (vehicleParts.Length != 5)
            {
                return false;
            }

            if (!int.TryParse(vehicleParts[0], out yearId) 
                || !int.TryParse(vehicleParts[1], out makeId) 
                || !int.TryParse(vehicleParts[2], out modelId) 
                || !int.TryParse(vehicleParts[3], out subModelId)
                || !bool.TryParse(vehicleParts[4], out showUniversal))
            {
                return false;
            }

            return true;
        }

        public void SetVehicleIdToCookies(int yearId, int makeId, int modelId, int subModelId, bool showUniversal)
        {
            if (yearId <= 0 || makeId <= 0 || modelId <= 0 || subModelId <= 0)
            {
                return;
            }

            if (httpContext == null || httpContext.Request == null)
            {
                return;
            }

            var vehicleIdCookie = this.httpContext.Request.Cookies.Get(VEHICLE_ID_COOKIE_KEY);
            if (vehicleIdCookie != null)
            {
                httpContext.Response.Cookies.Remove(VEHICLE_ID_COOKIE_KEY);
            }

            vehicleIdCookie = new HttpCookie(VEHICLE_ID_COOKIE_KEY);
            vehicleIdCookie.Value = string.Format(VEHICLE_ID_COOKIE_PATTERN, yearId, makeId, modelId, subModelId, showUniversal);
            vehicleIdCookie.Expires = DateTime.UtcNow.AddHours(24 * 365);
            this.httpContext.Response.Cookies.Add(vehicleIdCookie);
        }

        public void SetVehicleSeoIdToCookies(int? yearId, int? makeId, int? modelId)
        {
            if (httpContext == null || httpContext.Request == null)
            {
                return;
            }

            var vehicleSeoIdCookie = this.httpContext.Request.Cookies.Get(VEHICLE_SEO_ID_COOKIE_KEY);
            if (vehicleSeoIdCookie != null)
            {
                httpContext.Response.Cookies.Remove(VEHICLE_SEO_ID_COOKIE_KEY);
            }

            vehicleSeoIdCookie = new HttpCookie(VEHICLE_SEO_ID_COOKIE_KEY);
            vehicleSeoIdCookie.Value = (yearId.HasValue ? "year=" + yearId.Value + "|" : string.Empty)
                + (makeId.HasValue ? "make=" + makeId.Value + "|" : string.Empty)
                + (modelId.HasValue ? "model=" + modelId.Value + "|" : string.Empty);
            vehicleSeoIdCookie.Value = vehicleSeoIdCookie.Value.TrimEnd(new[] { '|' });
            vehicleSeoIdCookie.Expires = DateTime.UtcNow.AddHours(1);
            this.httpContext.Response.Cookies.Add(vehicleSeoIdCookie);
        }

        public void SetVehicleNameToCookies(int year, string makeName, string modelName, string subModelName)
        {
            if (year <= 0 || string.IsNullOrEmpty(makeName) || string.IsNullOrEmpty(modelName) || string.IsNullOrEmpty(subModelName))
            {
                return;
            }

            if (httpContext == null || httpContext.Request == null)
            {
                return;
            }

            var vehicleNameCookie = this.httpContext.Request.Cookies.Get(VEHICLE_NAME_COOKIE_KEY);
            if (vehicleNameCookie != null)
            {
                httpContext.Response.Cookies.Remove(VEHICLE_NAME_COOKIE_KEY);
            }

            vehicleNameCookie = new HttpCookie(VEHICLE_NAME_COOKIE_KEY);
            vehicleNameCookie.Value = string.Format(VEHICLE_NAME_COOKIE_PATTERN, year, makeName, modelName, subModelName);
            vehicleNameCookie.Expires = DateTime.UtcNow.AddHours(24 * 365);
            this.httpContext.Response.Cookies.Add(vehicleNameCookie);
        }

        public void SetVehicleSeoNameToCookies(int? year, string makeName, string modelName)
        {
            if (httpContext == null || httpContext.Request == null)
            {
                return;
            }

            var vehicleSeoNameCookie = this.httpContext.Request.Cookies.Get(VEHICLE_SEO_NAME_COOKIE_KEY);
            if (vehicleSeoNameCookie != null)
            {
                httpContext.Response.Cookies.Remove(VEHICLE_SEO_NAME_COOKIE_KEY);
            }

            vehicleSeoNameCookie = new HttpCookie(VEHICLE_SEO_NAME_COOKIE_KEY);
            vehicleSeoNameCookie.Value = (year.HasValue ? "year=" + year.Value + "|" : string.Empty)
                + (string.IsNullOrEmpty(makeName) ? string.Empty : "make=" + makeName + "|")
                + (string.IsNullOrEmpty(modelName) ? string.Empty : "model=" + modelName + "|");
            vehicleSeoNameCookie.Value = vehicleSeoNameCookie.Value.TrimEnd(new[] { '|' });
            vehicleSeoNameCookie.Expires = DateTime.UtcNow.AddHours(1);
            this.httpContext.Response.Cookies.Add(vehicleSeoNameCookie);
        }

        public void SetVehicleToCookies(Vehicle vehicle, bool showUniversal)
        {
            if (vehicle == null || vehicle.BaseVehicle == null || vehicle.BaseVehicle.Year == null 
                || vehicle.BaseVehicle.Make == null || vehicle.BaseVehicle.Model == null || vehicle.SubModel == null)
            {
                return;
            }

            this.SetVehicleIdToCookies(vehicle.BaseVehicle.YearId, vehicle.BaseVehicle.MakeId, vehicle.BaseVehicle.ModelId, vehicle.SubModelId, showUniversal);
            this.SetVehicleNameToCookies(vehicle.BaseVehicle.Year.Id, vehicle.BaseVehicle.Make.Name, vehicle.BaseVehicle.Model.Name, vehicle.SubModel.Name);
        }

        public void ClearVehicleCookies()
        {
            var vehicleIdCookie = this.httpContext.Request.Cookies.Get(VEHICLE_ID_COOKIE_KEY);
            if (vehicleIdCookie != null)
            {
                vehicleIdCookie.Expires = new DateTime(1970, 1, 1);
                vehicleIdCookie.Value = string.Empty;
                httpContext.Response.Cookies.Add(vehicleIdCookie);
            }

            var vehicleNameCookie = this.httpContext.Request.Cookies.Get(VEHICLE_NAME_COOKIE_KEY);
            if (vehicleNameCookie != null)
            {
                vehicleNameCookie.Expires = new DateTime(1970, 1, 1);
                vehicleNameCookie.Value = string.Empty;
                httpContext.Response.Cookies.Add(vehicleNameCookie);
            }
        }

        #endregion
    }
}