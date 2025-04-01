using Asu.Core.Domain.Seo;

namespace Asu.Data.Mapping.Vehicles
{
    public class VehicleUrlRecordMap : NopEntityTypeConfiguration<VehicleUrlRecord>
    {
        public VehicleUrlRecordMap()
        {
            this.ToTable("WCS_VehicleUrlRecord");
            this.HasKey(t => t.Id);
        }
    }
}
