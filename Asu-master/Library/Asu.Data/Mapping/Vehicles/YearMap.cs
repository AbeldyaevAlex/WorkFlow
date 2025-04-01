using Asu.Core.Domain.Vehicles;

namespace Asu.Data.Mapping.Vehicles
{
    public class YearMap : NopEntityTypeConfiguration<Year>
    {
        public YearMap()
        {
            this.ToTable("WCS_Year");
            this.HasKey(i => i.Id);
        }
    }
}
