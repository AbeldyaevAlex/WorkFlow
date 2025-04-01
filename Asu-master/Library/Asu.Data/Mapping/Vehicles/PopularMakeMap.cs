using Asu.Core.Domain.Vehicles;

namespace Asu.Data.Mapping.Vehicles
{
    public class PopularMakeMap : NopEntityTypeConfiguration<PopularMake>
    {
        public PopularMakeMap()
        {
            this.ToTable("WCS_PopularMakes");
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.Year, m.MakeId });
        }
    }
}
