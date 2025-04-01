namespace Asu.Core.Domain.Vehicles
{
    public class Model : BaseEntity
    {
        public string Name { get; set; }
        public bool IsActiveForFilter { get; set; }
        public bool IsActiveForSeo { get; set; }
        public bool IsActiveForSolrKeywordSearch { get; set; }

        public int VehicleTypeGroupId { get; set; }
    }
}
