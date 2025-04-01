namespace Asu.Web.Models.Vehicles
{
    public class VehicleSeoModel
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string EntityTitle { get; set; }
        public int MakeId { get; set; }
        public string MakeName { get; set; }
        public int? ModelId { get; set; }
        public string ModelName { get; set; }
        public int? YearId { get; set; }
        public string YearName { get; set; }
        public string Description { get; set; }
        public string Name { get { return string.Format("{0} {1}", this.VehicleName, this.EntityTitle).Replace("  ", " ").Trim(); } }
        public string VehicleName { get { return string.Format("{0} {1} {2}", this.YearName, this.MakeName, this.ModelName).Replace("  ", " ").Trim(); } }
    }
}